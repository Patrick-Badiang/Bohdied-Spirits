using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ScriptableObject/Equipment"))]
public class Equipment : ScriptableObject
{
    
    public EquipmentSlot equipmentSlot;

    public int damagemodifier;
    public int armormodifier;


    
}

public enum EquipmentSlot{head, sword, bow, chest, legs}
