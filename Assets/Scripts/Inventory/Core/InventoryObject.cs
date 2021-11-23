using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public enum InterfaceType{
    Inventory,
    Equipment,
    Ability,
    Description,
}




[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObject/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    
    public string savePath;

    const int SLOTID = 0;
    public ItemDataBaseObject dataBase;
    public InterfaceType type;

    public Inventory Container;

    public Ability _default;


    public InventorySlot[] GetSlots { get { return Container.Slots; } }
    //Allows us to not have to go inside the "Container" to get the slots
    //Made a "GetSlots" method to allow us to get the slot 

    public bool AddItem(Item _item, int _amount){

        //Check if there is an avaible slot to put an item in., if so add item to empty slot. 
        //Check if the item matches any other item in the inventory and is stackable/ has no buffs, if so then stack items
        if(EmptySlotCount == 0 )
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if(!dataBase.ItemObjects[_item.Id].stackable || slot == null){
            SetEmptySlot(_item, _amount);
            return true;
            //If we made it through all this then that means we did find a stackable item and the slot is not null
        }

        slot.AddAmount(_amount);
        return true; //Because we were able to find the item
    }

    

    public int EmptySlotCount{
        get{
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if(GetSlots[i].item.Id <= SLOTID){ //Checks if the slot is empty
                    counter++;
                }
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            
            if(GetSlots[i].item.Id == _item.Id){
                return GetSlots[i];
            }
            
        }

        return null;
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if(GetSlots[i].item.Id <= SLOTID){
                GetSlots[i].UpdateSlot( _item, _amount);
                return GetSlots[i];
            }
        }

        //Set up function for when inventory is full
        return null;
    }


    public void SwapItems(InventorySlot item1, InventorySlot item2){
        if(item2.CanPlaceInSlot(item1.itemObject) && item1.CanPlaceInSlot(item2.itemObject)){
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
        
    }

    public void RemoveItem(Item _item){
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if(GetSlots[i].item == _item){
                GetSlots[i].UpdateSlot(null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save(){
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load(){

        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }
            stream.Close();

        }

    }

    [ContextMenu("Clear")]
    public void Clear(){
        Container.Clear();
        if(type == InterfaceType.Ability){

            for (int i = 0; i < Container.Slots.Length; i++)
            { 
                GetSlots[i].UpdateSlot( new Item(_default), 1);

               
            }
        }
        
    }
}

[System.Serializable]
public class Inventory{

    public InventorySlot[] Slots = new InventorySlot[40];


    public void Clear(){
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
            
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot{

    public ItemType[] AllowedItems = new ItemType[0];

    [System.NonSerialized]
    public UserInterface parents;
    [System.NonSerialized]
    public GameObject slotDisplay;

    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;

    public Item item = new Item();
    public int amount;

    public ItemObject itemObject{
        get{
            if(item.Id >= 0)
            {return parents.inventory.dataBase.ItemObjects[item.Id];
            }
            return null;
        }
    }

    public InventorySlot(){
        UpdateSlot(new Item(), 0);
    }

    public InventorySlot(Item _item, int _amount){
        UpdateSlot(_item, _amount);
    }


    public void UpdateSlot(Item _item, int _amount){
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);

    }

    public void RemoveItem(){
        UpdateSlot(new Item(), 0);
    }

    public void AddAmount(int value){
        UpdateSlot(item, amount += value);
        
    }

    public bool CanPlaceInSlot(ItemObject _itemObject){
        if(AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 1)
        return true;

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if(_itemObject.type == AllowedItems[i])
            return true;
        }

        return false;
    }

    
}