using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplayModel : MonoBehaviour
{
    
    public Transform playerDisplayLookPoint;

    public void RotateRight(){
        playerDisplayLookPoint.Rotate(0, 90, 0);
    }

    public void RotateLeft(){
        playerDisplayLookPoint.Rotate(0, -90, 0);
    }
}
