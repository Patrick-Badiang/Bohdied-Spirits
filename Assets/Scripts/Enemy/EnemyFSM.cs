using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState{wandering, chasing, attacking, idle}
    public enum EnemyLevel{midSpirit, ElementalSpirit, Boss}

    EnemyState enemyState;
    public EnemyLevel enemyLevel;
    public Sight sight;

    public float attackDistance;
    public Transform attackTransform;
    

    float lastAttackTime;
    public float attackSpeed;
    EnemyBase combat;
    NavMeshAgent agent;

    void Awake(){
        
        agent = GetComponentInParent<NavMeshAgent>();
        combat = GetComponentInParent<EnemyBase>();
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
        if(sight.detectedObject == null){
            enemyState = EnemyState.wandering;
            return;
        }

        float distanceToPlayer = Vector3.Distance(attackTransform.position, sight.detectedObject.transform.position);
        if(distanceToPlayer > attackDistance * 1.1f){
            enemyState = EnemyState.chasing;
        }

        agent.isStopped = true;

        combat.DoDamage(sight.detectedObject);
    }
    void Idle(){}

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackTransform.position, attackDistance);

        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

}
