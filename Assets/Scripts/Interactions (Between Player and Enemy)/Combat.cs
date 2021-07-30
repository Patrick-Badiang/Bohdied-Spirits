using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public int damage;
    public int armor;
    public int attackSpeed;


    [Header("Animations")]
    public float deathDelay;
    public float attackDuration;

    [Header("Elements")]
    public float vulnerableDamage;
    public ElementType elementType;
    public ElementType[] vulnerableElementType;


    bool vulnerable;
    float lastAttackTime;


    public void DoDamage(Collider other){
        StartCoroutine(AttackAnimation());
        PlayerStats healthScript = other.GetComponent<PlayerStats>();
        
        if(healthScript != null){
            if(Time.timeScale > 0){
            var timeSinceLastShoot = Time.time - lastAttackTime;
            if(timeSinceLastShoot < attackSpeed)
                return;
            
            lastAttackTime = Time.time;
            healthScript.TakeDamage(damage);
            }
        }
    }

    IEnumerator AttackAnimation(){
        // Make animator parameter set to true

        yield return new WaitForSeconds(attackDuration);
    }

    public void TakeDamage(float damage, ElementType passed_elementType){

        damage -= armor;
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //Makes sure that the damage never reaches below zero

        if(CheckElement(passed_elementType)){ damage *= vulnerableDamage;}

        if(health > 0)
        health -= damage;
        
        
        if(health <= 0){
            Die();
        }
    }

    bool CheckElement(ElementType passed_elementType){
        for(int i = 0; i < vulnerableElementType.Length; i++){
            if(vulnerableElementType[i] == passed_elementType){
                return true;
                
            }
            return false;      
        }
        return false;
    }

    void Die(){

       gameObject.SetActive(false);
        
    }
}
