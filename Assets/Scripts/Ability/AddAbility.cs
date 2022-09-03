using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAbility : MonoBehaviour
{

    [SerializeField]
    private VoidEvent _onClicked;
    public InventoryObject _PlayerAbilityList;
    public Ability _ContractAbility;

    public Ability _KillAbility;
    public void ContractClicked(){
        //get player ability list
        // Debug.Log(_PlayerAbilityList.GetSlots.Length);
        //Check for empty slots
        //When it finds the first empty slot it stops looping
        //Then Adds the given ability to the player ability list 

        if(_PlayerAbilityList.AddItem(new Item(_ContractAbility), 1)){
            _onClicked.Raise();
        }
        //causing the empty slot to then have the new ability
    }

    public void KillClicked(){
        if(_PlayerAbilityList.AddItem(new Item(_KillAbility), 1)){
            _onClicked.Raise();
        }
    }
}
