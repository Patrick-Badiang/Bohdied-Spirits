using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFlipper : MonoBehaviour
{
    bool uiStatus;
    public void DisplayInventory(){
        gameObject.SetActive(!gameObject.activeSelf);
            uiStatus = !uiStatus;
    }
}
