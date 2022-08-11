using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RayCastAction : MonoBehaviour
{
    

    void Action(){

    }

    public void Launch(){
        Debug.Log("launched");
        // Vector3 fwd = transform.TransformDirection(Vector3.forward);

        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)){
            Debug.Log("hit");
        }

    }

    void Update(){
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }
}
