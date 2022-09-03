using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShootTriggerable : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    public Transform bulletSpawn;
    [HideInInspector] public float forceValue = 1f;

    public void Launch(){
        Ray dirct = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Rigidbody clonedObject = Instantiate(rb, dirct.origin, bulletSpawn.rotation) as Rigidbody;

        clonedObject.AddForce(dirct.direction * forceValue);

        
    }
}
