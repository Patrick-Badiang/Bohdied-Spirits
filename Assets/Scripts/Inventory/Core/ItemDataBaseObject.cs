using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item DataBase", menuName = "ScriptableObject/Inventory System/Items/Database")]
public class ItemDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    
    public ItemObject[] ItemObjects;

    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize(){
        for (int i = 0; i < ItemObjects.Length; i++)
        {
            ItemObjects[i].data.Id = i;

            //GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize(){
        //GetItem = new Dictionary<int, ItemObject>();
    }

    //Unity was re-serializing the inventory with the Items orignal values instead of the default valyues that was set after creating the 
    //items inside the inventory, so forcing a re-serialization by manually changing another value possibly fixed it
    
}
