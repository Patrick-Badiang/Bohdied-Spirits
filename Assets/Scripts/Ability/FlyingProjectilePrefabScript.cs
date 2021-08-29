using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingProjectilePrefabScript : MonoBehaviour
{
    Collider _collider;

    public float damage;
    public float durationOfAbility;
    public ElementType elementType;
    public string whatIsPlayer;
    public string whatIsItem;


    IEnumerator Start(){

        yield return new WaitForSeconds(durationOfAbility); 
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if((other.tag != whatIsPlayer) && (other.tag != whatIsItem)){gameObject.SetActive(false);}
        
        
        

        if(other.tag == "Enemy"){
            Combat enemyScript = other.GetComponent<Combat>();
            enemyScript.TakeDamage(damage, elementType);
        }
    }
}
