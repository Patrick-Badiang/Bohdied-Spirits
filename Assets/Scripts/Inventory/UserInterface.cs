using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour
{

    public MouseItem mouseItem = new MouseItem();
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    bool inventoryStatus;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    
    
    void Start()
    {
        CreateSlots();
    }

    
    
    void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots(){
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.ID >= 0){ //Checking if the slot has an item in it
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.GetItem[_slot.Value.item.Id].uiDisplay;

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

    public void CreateSlots(){
        
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            
            AddEvent(obj, EventTriggerType.PointerEnter, delegate {OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate {OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate {OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate {DragExit(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate {OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }

    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj){
        mouseItem.hoverObj = obj;
        if(itemsDisplayed.ContainsKey(obj)){ //Checks if the slot has something inside of it.
            mouseItem.hoverItem = itemsDisplayed[obj];
        }

    }

    public void OnExit(GameObject obj){
        //Check if there is a slot
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
        
    }

    public void OnDragStart(GameObject obj){
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(110,110);
        mouseObject.transform.SetParent(transform.parent);

        if(itemsDisplayed[obj].ID >= 0){
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.dataBase.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }

        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }

    public void DragExit(GameObject obj){
        if(mouseItem.hoverObj){
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else{
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }



    public void OnDrag(GameObject obj){
        if(mouseItem.obj != null){
            mouseItem.obj.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        }
    }



    public void LoadInventory(){
         gameObject.SetActive(!gameObject.activeSelf);
            inventoryStatus = !inventoryStatus;

            // if (!inventoryStatus){
            //     // cameraScript.enabled = false;
            //     Cursor.lockState = CursorLockMode.None;
            // }else{
            //     // cameraScript.enabled = true;
            //     Cursor.lockState = CursorLockMode.Locked;
            // }
    }    

    public Vector3 GetPosition(int i){
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)), 0f);
    }
}

public class MouseItem{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}

