using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryInterface : DynamicInterface
{

    public InventoryObject attributeDescriptions;

    public TextMeshProUGUI itemName;
    public override void CreateSlots(){
        
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);

            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate {OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate {OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate {OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate {DragExit(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate {OnDrag(obj); });

            inventory.GetSlots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }

    }
    public void OnEnter(GameObject obj){
        MouseData.slotHoveredOver = obj;
        StopAllCoroutines();

        if(attributeDescriptions != null){
            // Debug.Log(MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver].item);
        var cut = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver].item;
        itemName.text = cut.Name;
        for (int i = 0; i < cut.buffs.Length; i++) //For each buff on the item it executes the code
                {
                    Item temp = new Item();
                    temp.Name = cut.buffs[i].attribute.ToString();
                    temp.Id = cut.Id;
                    if(cut.Id > -1){

                        StartCoroutine(Wait(temp, cut.buffs[i].value, attributeDescriptions));}
                    // AttributeDescription.TextParameters(cut.buffs[i].value, cut.buffs[i].attribute);

                }
        
        
        }
    }

    public void OnExit(GameObject obj){
        StopAllCoroutines();

        itemName.text = null;
        //Check if there is a slot
        if(attributeDescriptions != null){
        attributeDescriptions.Clear();
        }

        MouseData.slotHoveredOver = null;
    }
}
