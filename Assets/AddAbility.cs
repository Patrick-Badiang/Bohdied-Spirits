using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbility : MonoBehaviour
{

    [SerializeField]
    private VoidEvent _onClicked;
    public InventoryObject _PlayerAbilityList;
    public Ability _abilityToAdd;
    public void Clicked(){
        //get player ability list
        // Debug.Log(_PlayerAbilityList.GetSlots.Length);
        //Check for empty slots
        //When it finds the first empty slot it stops looping
        //Then Adds the given ability to the player ability list 

        if(_PlayerAbilityList.AddItem(new Item(_abilityToAdd), 1)){
            _onClicked.Raise();
            
        }
        //causing the empty slot to then have the new ability
    }
}
