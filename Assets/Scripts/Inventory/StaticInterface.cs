using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class StaticInterface : UserInterface
{
    public GameObject[] slots;


    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>(); //Makes sure that there are no current links between Equipment Database and our equipment display;
        for (int i = 0; i < inventory.Container.Items.Length; i++) //Loop through all equipment in our database
        {
            var obj = slots[i]; //links the obj to each gameobject in the array
                                //Take the slot prefabs, link them to the same slot in the database
                            

            AddEvent(obj, EventTriggerType.PointerEnter, delegate {OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate {OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate {OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate {DragExit(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate {OnDrag(obj); });

            slotsOnInterface.Add(obj, inventory.Container.Items[i]); //Add them to the actual items displayed
        }
    }
}
