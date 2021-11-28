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

    public GameObject muzzleFlash;
    public GameObject hitFlash;


    IEnumerator Start(){
        var muzzleVFX = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
        muzzleVFX.transform.forward = gameObject.transform.position;
        yield return new WaitForSeconds(durationOfAbility); 
        gameObject.SetActive(false);
    }

    // void OnTriggerEnter(Collider other){

        
        
    //     if((other.tag != whatIsPlayer) && (other.tag != whatIsItem)){
    //         gameObject.SetActive(false);
    //         Debug.Log("hit");
    //         Instantiate(hitFlash, pos, rot);

                
    //         }
        
        
        

    //     if(other.tag == "Enemy"){
    //         Combat enemyScript = other.GetComponent<Combat>();
    //         enemyScript.TakeDamage(damage, elementType);
    //     }
    // }

    void OnCollisionEnter(Collision other){
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if((other.gameObject.tag != whatIsPlayer) && (other.gameObject.tag != whatIsItem)){
            gameObject.SetActive(false);
            Instantiate(hitFlash, pos, rot);

                
        }

        if(other.gameObject.tag == "Enemy"){
            Combat enemyScript = other.gameObject.GetComponent<Combat>();
            enemyScript.TakeDamage(damage, elementType);
        }
    }
}
