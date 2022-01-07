using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerStats : MonoBehaviour
{
    public InventoryObject equipment;
    public VoidEvent onDeath;

    public FloatVariable health;
    public FloatVariable maxHealth;
    

    public int baseDamage;

    private BoneCombiner _boneCombiner;
    private PlayerCombat playerCombat;

    public Attribute[] attributes;

    private Transform _helmet;
    private Transform _chestplate;
    private Transform _leggings;
    private Transform _weapon;

    public Transform weaponHoldTransform;

    

    public float currentHealth{get; private set; }

    

    public void TakeDamage(float damage){
        float takendamage = damage * (1 - GetValue(0));
        takendamage = Mathf.Clamp(takendamage, 0, int.MaxValue);
        
        health.ApplyChange(-takendamage);

        if(health.Value <= 0 ){
            onDeath.Raise();
        }
    }

    
    private void Start(){

        health.SetValue(maxHealth);

        _boneCombiner = new BoneCombiner(gameObject);
        playerCombat = GetComponent<PlayerCombat>();
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
            equipment.GetSlots[i].OnAfterUpdate += OnAddItem;

        }
    }
    
    public void OnRemoveItem(InventorySlot _slot){

        if(_slot.itemObject == null) return;
        switch (_slot.parents.inventory.type)
        {
            
            case InterfaceType.Inventory:
            break;

            case InterfaceType.Equipment:

                for (int i = 0; i < _slot.item.buffs.Length; i++) //For each buff on the item it executes the code
                {
                    for (int j = 0; j < attributes.Length; j++) //Checks each buffs attribute
                    {
                        
                        if(attributes[j].type == _slot.item.buffs[i].attribute) //Then comapres the attributes to the attributes on the character
                        attributes[j].value.RemoveModifier(_slot.item.buffs[i]); //Finally removes the attribute to the character
                    }
                }

                
                if(_slot.itemObject.characterDisplay != null){
                    
                    switch(_slot.AllowedItems[0]){
                        case ItemType.Helmet:
                            Destroy(_helmet.gameObject);
                        break;

                        case ItemType.Chestplate:
                            Destroy(_chestplate.gameObject);
                        break;

                        case ItemType.Leggings:
                            Destroy(_leggings.gameObject);
                        break;

                        case ItemType.Weapon:
                            Destroy(_weapon.gameObject);
                            // playerCombat.attackRate = playerCombat._default;
                        break;
                    }                    
                }
                
            break;
            
        }
    }

    public void OnAddItem(InventorySlot _slot){
        if(_slot.itemObject == null) return;
        switch (_slot.parents.inventory.type)
        {
            
            case InterfaceType.Inventory:
            break;

            case InterfaceType.Equipment:

                for (int i = 0; i < _slot.item.buffs.Length; i++) //For each buff on the item it executes the code
                {
                    for (int j = 0; j < attributes.Length; j++) //Checks each buffs attribute
                    {
                        if(attributes[j].type == _slot.item.buffs[i].attribute) //Then comapres the attributes to the attributes on the character
                        attributes[j].value.AddModifier(_slot.item.buffs[i]); //Finally adds the attribute to the character
                    }
                }

                
                if(_slot.itemObject.characterDisplay != null){
        
                    switch(_slot.AllowedItems[0]){
                        case ItemType.Helmet:
                            _helmet = _boneCombiner.AddLimb(_slot.itemObject.characterDisplay, _slot.itemObject.boneNames);
                        break;

                        case ItemType.Chestplate:
                            _chestplate = _boneCombiner.AddLimb(_slot.itemObject.characterDisplay,  _slot.itemObject.boneNames);
                        break;

                        case ItemType.Leggings:
                            _leggings= _boneCombiner.AddLimb(_slot.itemObject.characterDisplay,  _slot.itemObject.boneNames);
                        break;

                        case ItemType.Weapon:
                            var newObject = Instantiate(_slot.itemObject.characterDisplay, weaponHoldTransform, false);
                            // newObject.layer = LayerMask.NameToLayer("Equipment");
                            _weapon = newObject.transform;
                            playerCombat.attackRate = _slot.item.AttackRate;

                        break;
                    }  
                }
            break;
            
        }
    }

    public float GetValue(int i){
        float modifiedValue =  attributes[i].value.ModifiedValue/100f;
    

        return modifiedValue;
    }


    public void AttributeModified(Attribute attribute){
        // Debug.Log(string.Concat(attribute.type, " was updated! Value: ",attribute.value.ModifiedValue ));
    }
}

[System.Serializable]
public class Attribute{
    [System.NonSerialized]
    public PlayerStats parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(PlayerStats _parent){
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified(){
        parent.AttributeModified(this);
    }
}