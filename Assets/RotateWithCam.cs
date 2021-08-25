using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithCam : MonoBehaviour
{
   
    public GameObject followTransform;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

    }
}
