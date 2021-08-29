using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform _cam;

    public void Awake(){
        _cam = Camera.main.transform;
    }

    public void LateUpdate(){
        transform.LookAt(transform.position + _cam.forward);
    }
}
