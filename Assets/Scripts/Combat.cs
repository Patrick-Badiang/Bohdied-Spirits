using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public bool isPlayer;
    [Header("Stats")]
    public float health;
    public float damage;
    public float attackSpeed;

    [Header("Animations")]
    public float deathDelay;
    //public float attackDuration;

    [Header("Elements")]
    public ElementType elementType;
    public ElementType[] vulnerableElementType;


    bool vulnerable;
    float lastAttackTime;


    public void DoDamage(Collider other){
        //StartCoroutine(AttackAnimation());
        Combat healthScript = other.GetComponent<Combat>();
        if(healthScript != null){
            if(Time.timeScale > 0){
            var timeSinceLastShoot = Time.time - lastAttackTime;
            if(timeSinceLastShoot < attackSpeed)
                return;
            
            lastAttackTime = Time.time;
            healthScript.TakeDamage(damage, elementType);
            }
        }
    }

    //IEnumerator AttackAnimation(){
        //Make animator parameter set to true

        //yield return new WaitForSeconds(attackDuration);
    //}

    public void TakeDamage(float passed_Damage, ElementType passed_elementType){
        CheckElement();

        if(vulnerable){ damage *= 2;}

        if(health > 0)
        health -= damage;
        
        if(health <= 0){
            Die();
        }
    }

    void CheckElement(){
        for(int i = 0; i < vulnerableElementType.Length; i++){
            if(vulnerableElementType[i] == elementType){
                vulnerable = true;
            }
            vulnerable = false;      
        }
    }

    void Die(){

        if(!isPlayer){gameObject.SetActive(false);}
        if(isPlayer & Time.timeScale > 0){Debug.Log("Dead"); Time.timeScale = 0;}
        
    }
}
