# AI Survival

 "Better late than never" ahh project

This project features a zenithal view simple test game to test en ememy with some behaviours typical of a zombie survival game.  

## States:  
### Patrol State  
Goes from PatrolA to PatrolB constantly until its vision cone detects the player  
  
### Chase State  
When the player is detected, the enemy follows the player during 5 seconds. After this 5 seconds, if the player is still visible in the vision cone, the enemy re-enters this state, if the player isn't detected, the enemy goes back to the state of patrol  

### Flee State
When the player is detected and the enemy is at 50HP or less, the enemy starts running away from the player during 5 seconds. If the player re-enters the vision cone after this tame, the state reactivates. Else it turns back to the patrol state until the player is detected again.  
  
### Death State  
Simple state, when the enemy is at 0 or less HP, it stops and destroys itself

## Controls:  
-To move, use WASD controls  
-To damage the enemy and test its Flee State, press Spacebar (It takes 51 life points, so pressing it a second times kills the enemy)
  

Important: There are 2 scripts attached to the enemy, "EnemyBehavior", which is a finite state machine using an enum class, and "EnemyBehaviourFSM", this one should be active by default, this one is a version where the states are separated between multiple scripts to test entering and leaving states
