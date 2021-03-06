using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Helmet,
    Weapon,
    Chestplate,
    Leggings,
    Default,
    ProjectileAbility,
    RaycastAbility,
}

public enum Attributes{
    Defense,
    Strength,
    Agility,
    Health,
}

public abstract class ItemObject : ScriptableObject
{

    public GameObject characterDisplay;
    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    public float attackRate;

    public AudioClip _sound;
    public float baseCooldown;

    public Rigidbody rb;

    // public abstract void Initialize(GameObject _obj);
    
    // public abstract void CastAbility();

    [TextArea(15,20)]
    public string description;

    public Item data = new Item();

    public List<string> boneNames = new List<string>();
    //This will change whenever we add or remove a characterDisplay

    public Item CreateItem(){
        Item newItem = new Item(this);
        return newItem;

    }

    

    private void OnValidate(){ //Runs everytime in play mode and out of play mode whenever a variable is changed

        boneNames.Clear();
        if(characterDisplay == null)
            return;
        
        if(!characterDisplay.GetComponent<SkinnedMeshRenderer>())
            return;
        
        var renderer = characterDisplay.GetComponent<SkinnedMeshRenderer>();
        var bones = renderer.bones;
        
        foreach (var t in bones)
        {
            boneNames.Add(t.name);
        }
    }
}

[System.Serializable]
public class Item{
    
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public float AttackRate;

    public AudioClip _sound;

    [HideInInspector]
    public float BaseCooldown;


    // public abstract void Initialize(GameObject _obj);
    
    // public abstract void CastAbility();


    public Item(){
        Name = "";
        Id = -1;

    }
    public Item(ItemObject item){
        Name = item.name;
        Id = item.data.Id;
        AttackRate = item.attackRate;
        buffs = new ItemBuff[item.data.buffs.Length];
        BaseCooldown = item.baseCooldown;

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);

            buffs[i].attribute = item.data.buffs[i].attribute;
        }
    }

    // public Item(Ability item){
    //     Name = item.name;
    //     Id = item.data.Id;
        
    //     Debug.Log("Ability made");
    // }
}

[System.Serializable]
public class ItemBuff : IModifier{

    public Attributes attribute;
    public int value;
    public int min,max;


    public ItemBuff(int _min, int _max){
        min = _min;
        max = _max;
        GenerateValue();
        
    }

    public void GenerateValue(){
        value = UnityEngine.Random.Range(min,max);
    }

    public void AddValue(ref int v){
        v += value;
    }
}
