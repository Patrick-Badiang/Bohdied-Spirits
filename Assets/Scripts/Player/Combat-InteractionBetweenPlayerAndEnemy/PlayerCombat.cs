using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private VoidEvent onAttack;
    private float coolDownDiration;
    private float nextReadyTime;
    private float coolDownTimeLeft;
    private bool canBetriggered;
    PlayerStats playerStats;

    public Transform meleePos;
    public float meleeRange;
    public LayerMask whatIsEnemy;

    public ElementType elementType;

    
    public void Awake(){
        playerStats = GetComponent<PlayerStats>();
    }

    void Update(){
        bool coolDownComplete = (Time.time > nextReadyTime);
        if(coolDownComplete){
            canBetriggered = true;
        }else{
            canBetriggered = false;
        }
    }

    public void CheckAttackCooldown(){
        if(canBetriggered){
            onAttack.Raise();
        }else{
            return;
        }
    }

    public void Attack(){

        Collider[] enemiesThatAreHit = Physics.OverlapBox(meleePos.position, meleePos.localScale/2, Quaternion.identity, whatIsEnemy);

        foreach (Collider enemy in enemiesThatAreHit)
        {
            enemy.GetComponent<Combat>().TakeDamage(DoMeleeDamage(playerStats.baseDamage), elementType);
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
