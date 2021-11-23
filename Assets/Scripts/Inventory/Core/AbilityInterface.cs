using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityInterface : StaticInterface
{

    // Ability settingparameters;

    [SerializeField]

    GameObject weaponHolder;
    
    public override void OnSlotUpdate(InventorySlot _slot){
        if(_slot.item.Id >= 0){ //Checking if the slot has an item in it
                // var tmp = ScriptableObject.CreateInstance<ProjectileAbility>();
                
                // tmp._sprite = _slot.itemObject.uiDisplay;
                // tmp.projectile = _slot.itemObject.rb;
                // tmp._sound = _slot.itemObject._sound;
                // tmp._baseCoolDown = _slot.itemObject.baseCooldown; 
                // settingparameters = tmp;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.itemObject.uiDisplay;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "": _slot.amount.ToString("n0");
                //Slot calls teh InitializeMethod and pass in (settingparameters).

                // settingparameters._sprite = _slot.itemObject.uiDisplay;
                // settingparameters._sound = _slot.itemObject._sound;
                // settingparameters._baseCoolDown = _slot.itemObject.baseCooldown;
                AbilityCoolDown slotScript =_slot.slotDisplay.transform.GetComponent<AbilityCoolDown>();
                if(slotScript){ slotScript.InitializeAbility(_slot,weaponHolder);}
                
                
            }
            else{
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";

            }
    }
}
