using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Food,
    Equipment,
    Default
}

public enum Attribute{
    Defense,
    Strength,
    Agility,
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;

    [TextArea(15,20)]
    public string description;

    public ItemBuff[] buffs;

    public Item CreateItem(){
        Item newItem = new Item(this);
        return newItem;

    }
}

[System.Serializable]
public class Item{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;

    public Item(ItemObject item){
        Name = item.name;
        Id = item.Id;
        buffs = new ItemBuff[item.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);

            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff{
    public Attribute attribute;
    public int value;
    public int min,max;

    public ItemBuff(int _min, int _max){
        min = _min;
        max = _max;
        GenerataeValue();
    }

    public void GenerataeValue(){
        value = UnityEngine.Random.Range(min,max);
    }
}
