using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StaticInterface : UserInterface
{
    public GameObject[] slots;


    

    public override void OnSlotUpdate(InventorySlot _slot){

        if(_slot.item.Id >= 0){ //Checking if the slot has an item in it
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.itemObject.uiDisplay;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "": _slot.amount.ToString("n0");
                
            }
            else{
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";

            }
    }
    public override void CreateSlots()
    {
        
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>(); //Makes sure that there are no current links between Equipment Database and our equipment display;
        for (int i = 0; i < inventory.GetSlots.Length; i++) //Loop through all equipment in our database
        {
            var obj = slots[i]; //links the obj to each gameobject in the array
                                //Take the slot prefabs, link them to the same slot in the database
                            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate {OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate {OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate {OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate {DragExit(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate {OnDrag(obj); });
            
            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]); //Add them to the actual items displayed
        }
    }
}


