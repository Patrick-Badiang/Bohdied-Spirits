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

    public void AddItem(Item _item, int _amount){

        if(_item.buffs.Length > 0){
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            return;
        }

        for (int i = 0; i < Container.Items.Count; i++)
        {
            if(Container.Items[i].item.Id == i){
                Container.Items[i].AddAmount(_amount);
                return;
            }

            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));

        }
    }

    [ContextMenu("Save")]
    public void Save(){
        IFormatter formatImporter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath), FileMode.Create, FileAccess.Write);
        formatImporter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load(){
        if(File.Exists(string.Concat(Application.persistentDataPath))){

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath ), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [ContextMenu("Clear")]
    public void Clear(){
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory{
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot{
    public int ID;
    public Item item;
    public int amount;

    public InventorySlot(int _id, Item _item, int _amount){
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value){
        amount += value;
    }
}