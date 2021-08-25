using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootTriggerable : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    public Transform bulletSpawn;
    [HideInInspector] public float forceValue = 1f;

    public void Launch(){
        Rigidbody clonedObject = Instantiate(rb, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody;

        clonedObject.AddForce(bulletSpawn.transform.forward * forceValue);
    }
}
