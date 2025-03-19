using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum state
{
    Damaged,
    Patrol,
    Chase,
    Flee
}

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public int life;
    public NavMeshAgent agent;
    public state state;

    public Transform player;
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public LayerMask obstacleMask;

    public Transform patrolA;
    public Transform patrolB;
    public Transform currentTarget;

    public int followStateTimer;
    void Start()
    {
        currentTarget = patrolA.transform;
        life = 100;
        speed *= 0.01f;
        agent = GetComponent<NavMeshAgent>();
        state = state.Patrol;
        agent.destination = currentTarget.position;
    }


    void Update()
    {
        if (state == state.Chase)
        {
            ChaseBehavior();
        }
        else if (state == state.Flee)
        {
            // Lógica para huir (si la agregas en el futuro)
        }
        else if (state == state.Patrol)
        {
            PatrolBehavior();
        }

        // Si el jugador es visto y el enemigo no estaba ya en persecución, cambia a Chase
        if (CanSeePlayer() && state != state.Chase)
        {
            state = state.Chase;
            currentTarget = player;
            StartCoroutine(followTimer());  // Inicia la corutina cuando comienza la persecución
        }

        if (life <= 0) GameObject.Destroy(gameObject);

        if (Input.GetKeyDown(KeyCode.Space)) life -= 51;
    }

    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Comprobar si el jugador está dentro del rango de visión
        if (distanceToPlayer < visionRange)
        {
            // Comprobar si el jugador está dentro del ángulo de visión
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer < visionAngle / 2)
            {
                // Comprobar si hay obstáculos entre el enemigo y el jugador
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void ChaseBehavior()
    {
        agent.destination = currentTarget.position;
    }

    public void PatrolBehavior()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentTarget = (currentTarget == patrolA) ? patrolB : patrolA;
            agent.SetDestination(currentTarget.position);
        }
    }

    public IEnumerator followTimer()
    {
        yield return new WaitForSeconds(followStateTimer);

        // Solo regresa a patrulla si sigue en estado de Chase (por si cambia a otro estado en medio del tiempo)
        if (state == state.Chase)
        {
            state = state.Patrol;
            currentTarget = patrolA; // O el punto más cercano de patrulla
            agent.SetDestination(currentTarget.position);
        }
    }

}
