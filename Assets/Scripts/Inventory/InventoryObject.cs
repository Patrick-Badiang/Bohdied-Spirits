using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObject/Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    
    public string savePath;
    public ItemDataBaseObject dataBase;

    public Inventory Container;

    public bool AddItem(Item _item, int _amount){

        //Check if there is an avaible slot to put an item in., if so add item to empty slot. 
        //Check if the item matches any other item in the inventory and is stackable/ has no buffs, if so then stack items
        if(EmptySlotCount <= 0 )
            return false;
        InventorySlot slot = FindItemOnInventory(_item);
        if(!dataBase.GetItem[_item.Id].stackable || slot == null){
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
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if(Container.Items[i].item.Id <= -1){ //Checks if the slot is empty
                    counter++;
                }
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item){
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item.Id == _item.Id){
                return Container.Items[i];
            }
            
        }

        return null;
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount){
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item.Id <= -1){
                Container.Items[i].UpdateSlot( _item, _amount);
                return Container.Items[i];
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
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].item == _item){
                Container.Items[i].UpdateSlot(null, 0);
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
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();

        }

    }

    [ContextMenu("Clear")]
    public void Clear(){
        Container.Clear();
    }
}

[System.Serializable]
public class Inventory{

    public InventorySlot[] Items = new InventorySlot[24];
    public void Clear(){
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(new Item(), 0);

        }
    }
}

[System.Serializable]
public class InventorySlot{

    public ItemType[] AllowedItems = new ItemType[0];

    public UserInterface parents;
    public Item item;
    public int amount;

    public ItemObject itemObject{
        get{
            if(item.Id >= 0)
            {return parents.inventory.dataBase.GetItem[item.Id];
            }
            return null;
        }
    }

    public InventorySlot(){
        item = null;
        amount = 0;
    }

    public InventorySlot(Item _item, int _amount){
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value){
        amount += value;
    }

    public void UpdateSlot(Item _item, int _amount){
        item = _item;
        amount = _amount;
    }

    public bool CanPlaceInSlot(ItemObject _itemObject){
        if(AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
        return true;

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if(_itemObject.type == AllowedItems[i])
            return true;
        }

        return false;
    }

    public void RemoveItem(){
        item = new Item();
        amount = 0;
    }
}