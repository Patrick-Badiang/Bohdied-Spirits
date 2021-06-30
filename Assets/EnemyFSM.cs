using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState{wandering, chasing, attacking, idle}
    public enum EnemyLevel{midSpirit, ElementalSpirit, Boss}

    EnemyState enemyState;
    EnemyLevel enemyLevel;
    public Sight sight;

    public float attackDistance;
    



    NavMeshAgent agent;

    void Awake(){
        agent = GetComponentInParent<NavMeshAgent>();
    
    }

    void Update(){
        if(enemyState == EnemyState.wandering){
            Wandering();
        }
        if(enemyState == EnemyState.chasing){
            Chasing();
        }
        if(enemyState == EnemyState.attacking){
            Attacking();
        }
        if(enemyState == EnemyState.idle){
            Idle();
        }

    }

    void Wandering(){
        agent.isStopped = false;
        if(sight.detectedObject != null){
            enemyState = EnemyState.chasing;
        }
    }
    void Chasing(){
        agent.isStopped = false;
        
        if(sight.detectedObject == null){
            enemyState = EnemyState.wandering;
            return;
        }

        agent.SetDestination(sight.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sight.detectedObject.transform.position);
        if(distanceToPlayer <= attackDistance){
            enemyState = EnemyState.attacking;
        }
    }

    void Attacking(){
        agent.isStopped = true;

        //Make this trigger an event and have another script listen the "OnAttack" event.
        Debug.Log("Attacking");
        if(sight.detectedObject == null){
            enemyState = EnemyState.wandering;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sight.detectedObject.transform.position);
        if(distanceToPlayer > attackDistance * 1.1f){
            enemyState = EnemyState.chasing;
        }
    }
    void Idle(){}

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

}
