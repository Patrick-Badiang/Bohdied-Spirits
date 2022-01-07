using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private VoidEvent onAttackAnim;
    
    private bool canBetriggered;
    PlayerStats playerStats;

    [HideInInspector]
    public float attackRate;
    [HideInInspector]
    public float _default = 3f;

    float nextAttackTime = 0f;

    public Transform meleePos;
    public float meleeRange;
    public LayerMask whatIsEnemy;

    public ElementType elementType;

    
    public void Awake(){
        attackRate = _default;
        playerStats = GetComponent<PlayerStats>();
    }

    void Update(){
        if(Time.time >= nextAttackTime){
            canBetriggered = true;
        }else{
            canBetriggered = false;
        }
    }

    public void CheckAttackCooldown(){
        if(canBetriggered){
            Attack();
            onAttackAnim.Raise();
            nextAttackTime = Time.time + 1f / attackRate;
        }else{
            return;
        }
    }

    private void Attack(){

        Collider[] enemiesThatAreHit = Physics.OverlapBox(meleePos.position, meleePos.localScale/2, Quaternion.identity, whatIsEnemy);

        foreach (Collider enemy in enemiesThatAreHit)
        {
            enemy.GetComponent<EnemyBase>().TakeDamage(DoMeleeDamage(playerStats.baseDamage), elementType);
        }
    }

    public float DoMeleeDamage(float damageBeingDone){
        float finaldamageBeingDone = damageBeingDone * (1 + playerStats.GetValue(1));

        return finaldamageBeingDone;
    }

    void OnDrawGizmos(){
        if(meleePos == null)    return;
            Gizmos.DrawWireCube(meleePos.position, meleePos.localScale);
    }
}
