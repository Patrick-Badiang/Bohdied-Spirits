using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = ("ScriptableObject/Items/New Equipment Item"))]

public class EquipmentObject : ItemObject
{
    public void Awake(){
        type = ItemType.Chestplate;
    }
}
