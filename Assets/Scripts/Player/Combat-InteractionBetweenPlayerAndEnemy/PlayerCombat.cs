using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{
    PlayerStats playerStats;

    public Transform meleePos;
    public float meleeRange;
    public LayerMask whatIsEnemy;

    public ElementType elementType;
    

    public void Awake(){
        playerStats = GetComponent<PlayerStats>();
    }

    public void Attack(){
        Collider[] enemiesThatAreHit = Physics.OverlapSphere(meleePos.position, meleeRange, whatIsEnemy);

        foreach (Collider enemy in enemiesThatAreHit)
        {
            enemy.GetComponent<Combat>().TakeDamage(DoMeleeDamage(playerStats.attributes[1].value.ModifiedValue), elementType);
        }
    }

    public float DoMeleeDamage(float damageBeingDone){
        float finaldamageBeingDone = damageBeingDone * (1 + playerStats.GetValue(1));
        Debug.Log(damageBeingDone);
        return finaldamageBeingDone;
    }

    void OnDrawGizmos(){
        if(meleePos == null)    return;
        Gizmos.DrawWireSphere(meleePos.position, meleeRange);
    }
}
