using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public Stat damage;
    public Stat armor;
    public int attackSpeed;


    [Header("Animations")]
    public float deathDelay;
    //public float attackDuration;

    [Header("Elements")]
    public float vulnerableDamage;
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
            healthScript.TakeDamage(damage.GetValue(), elementType);
            }
        }
    }

    //IEnumerator AttackAnimation(){
        //Make animator parameter set to true

        //yield return new WaitForSeconds(attackDuration);
    //}

    public void TakeDamage(int damage, ElementType passed_elementType){
        CheckElement(passed_elementType);

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //Makes sure that the damage never reaches below zero

        if(vulnerable){ damage *= 2;}

        if(health > 0)
        health -= damage;
        
        if(health <= 0){
            Die();
        }
    }

    void CheckElement(ElementType passed_elementType){
        for(int i = 0; i < vulnerableElementType.Length; i++){
            if(vulnerableElementType[i] == passed_elementType){
                vulnerable = true;
            }
            vulnerable = false;      
        }
    }

    void Die(){

       gameObject.SetActive(false);
        
    }
}
