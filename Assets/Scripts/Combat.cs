using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public bool isPlayer;
    [Header("Stats")]
    public float health;
    public float damage;

    [Header("Animations")]
    public float animationDelay;

    [Header("Elements")]
    public ElementType elementType;
    public ElementType[] vulnerableElementType;


    bool vulnerable;


    public void DoDamage(Collider other){
        Combat healthScript = other.GetComponent<Combat>();
        if(healthScript != null){
            healthScript.TakeDamage(damage, elementType);
        }
    }

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
