using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttributeDescriptionInterface : StaticInterface
{
    // Start is called before the first frame update
        public override void OnSlotUpdate(InventorySlot _slot){
            if(_slot.item.Id >= 0){ //Checking if the slot has an item in it
                AttributeDescriptions description = _slot.slotDisplay.transform.GetComponent<AttributeDescriptions>();
                if(description) description.TextParameters(_slot.amount.ToString(), _slot.item.Name);
            }
            else{
                AttributeDescriptions description = _slot.slotDisplay.transform.GetComponent<AttributeDescriptions>();
                if(description) description.TextParameters(null, null);

            }
        }

        public override void CreateSlots(){
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>(); //Makes sure that there are no current links between Equipment Database and our equipment display;
        for (int i = 0; i < inventory.GetSlots.Length; i++) //Loop through all equipment in our database
        {
            var obj = slots[i]; //links the obj to each gameobject in the array
                                //Take the slot prefabs, link them to the same slot in the database
            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]); //Add them to the actual items displayed
        }
        }

    
}
