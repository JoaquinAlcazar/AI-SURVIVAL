using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public int life;
    public Transform goal;
    NavMeshAgent agent;
    void Start()
    {
        goal = GameObject.Find("Player").transform;
        life = 100;
        speed *= 0.01f;
        agent = GetComponent<NavMeshAgent>();


    }

    
    void Update()
    {
        agent.destination = goal.position;
        if (life<=0) GameObject.Destroy(gameObject);

        if (Input.GetKeyDown(KeyCode.Space)) life -= 51;
    }

}
