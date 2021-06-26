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

    [Header("Stats")]
    public float speed;
    public float health;
    



    NavMeshAgent agent;

    void Awake(){
        agent = GetComponent<NavMeshAgent>();
    
    }

    void Update(){
        if(enemyState == EnemyState.wandering){

        }
        if(enemyState == EnemyState.chasing){

        }
        if(enemyState == EnemyState.attacking){

        }
        if(enemyState == EnemyState.idle){

        }

    }

    void Wandering(){
        agent.isStopped = false;

    }
    void Chasing(){}
    void Attacking(){}
    void Idle(){}



}
