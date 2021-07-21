using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UserInterface : MonoBehaviour
{

    public InventoryObject inventory;

    bool inventoryStatus;

    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    
    
    void Start()
    {
        
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parents = this;
            inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;

        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate {OnEnterinterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate {OnExitinterface(gameObject); });
    }

    private void OnSlotUpdate(InventorySlot _slot){
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


    public abstract void CreateSlots();

    public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnExitinterface(GameObject obj){
        MouseData.interfaceMouseIsOver = null;
    }

    public void OnEnterinterface(GameObject obj){
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }

    public void OnEnter(GameObject obj){
        MouseData.slotHoveredOver = obj;
    
    }

    public void OnExit(GameObject obj){
        //Check if there is a slot
        MouseData.slotHoveredOver = null;
        
        
    }

    public void OnDragStart(GameObject obj){

        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        
    }

    public GameObject CreateTempItem(GameObject objBeingDragged){

        GameObject tempItem = null;
        if(slotsOnInterface[objBeingDragged].item.Id >= 0){
            
            tempItem = new GameObject();

            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(110,110);
            tempItem.transform.SetParent(transform.parent);

            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[objBeingDragged].itemObject.uiDisplay;
            img.raycastTarget = false;
        }

        return tempItem;

        
    }

    public void DragExit(GameObject obj){

       
        Destroy(MouseData.tempItemBeingDragged); 

        //Check if the mouse is over an interface, if it is not then we can just destroy the item
        //If the mouse is over a slot then we drop it, Call a "SwapItems functions" checks if the item can be dropped in the slot
        //Checks if there is an item in the slot already, if so then we have to check if they are able to be swapped
        if(MouseData.interfaceMouseIsOver == null){
            //obj is the original object that we started dragging.
            slotsOnInterface[obj].RemoveItem();
            return; //We return because if we remove the item then we do not need to swap the item
        }
        if(MouseData.slotHoveredOver){
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
            //We are pushing the slotsOnInterface with the object that is passed into the method (The object that we started dragging)
            //Then we look inside the dictionary of lengths for the item we started dragging
            //Then we simply try to swap it with the variable we set beforehand.
        }

    }

    



    public void OnDrag(GameObject obj){
        if(MouseData.tempItemBeingDragged != null){
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        }
    }



    public void LoadInventory(){
         gameObject.SetActive(!gameObject.activeSelf);
            inventoryStatus = !inventoryStatus;

            
    }    

    
}

public static class MouseData{

    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}

public static class ExtensionMethods{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface){
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if(_slot.Value.item.Id >= 0){ //Checking if the slot has an item in it
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.itemObject.uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "": _slot.Value.amount.ToString("n0");


            }
            else{
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";

            }
        }
    }
}

