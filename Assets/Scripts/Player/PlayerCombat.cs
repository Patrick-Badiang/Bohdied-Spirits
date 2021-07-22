using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public InventoryObject equipment;

    public Attribute[] attributes;

    private void Start(){
        for (int i = 0; i < attributes.Length-1; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;

        }
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot){

        if(_slot.itemObject == null) return;
        switch (_slot.parents.inventory.type)
        {
            
            case InterfaceType.Inventory:
            break;

            case InterfaceType.Equipment:
                Debug.Log("Removed item");

                for (int i = 0; i < _slot.item.buffs.Length; i++) //For each buff on the item it executes the code
                {
                    for (int j = 0; j < attributes.Length; j++) //Checks each buffs attribute
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute) //Then comapres the attributes to the attributes on the character
                        attributes[j].value.RemoveModifier(_slot.item.buffs[i]); //Finally removes the attribute to the character
                    }
                }
                
            break;
            
        }
    }

    public void OnAfterSlotUpdate(InventorySlot _slot){
        if(_slot.itemObject == null) return;
        switch (_slot.parents.inventory.type)
        {
            
            case InterfaceType.Inventory:
            break;

            case InterfaceType.Equipment:
                Debug.Log("Placed item");

                for (int i = 0; i < _slot.item.buffs.Length; i++) //For each buff on the item it executes the code
                {
                    for (int j = 0; j < attributes.Length; j++) //Checks each buffs attribute
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute) //Then comapres the attributes to the attributes on the character
                        attributes[j].value.AddModifier(_slot.item.buffs[i]); //Finally adds the attribute to the character
                    }
                }
            break;
            
        }
    }


    public void AttributeModified(Attribute attribute){
        Debug.Log(string.Concat(attribute.type, " was updated! Value: ",attribute.value.ModifiedValue ));
    }
}

[System.Serializable]
public class Attribute{
    [System.NonSerialized]
    public PlayerCombat parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerCombat _parent){
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified(){
        parent.AttributeModified(this);
    }
}