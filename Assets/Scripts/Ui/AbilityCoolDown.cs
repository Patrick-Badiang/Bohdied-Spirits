using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCoolDown : MonoBehaviour
{
    
    public Image darkMask;
    public TextMeshProUGUI coolDownTextDisplay;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject weaponHolder;
    private Image myButtonImage;
    private AudioSource abilitySource;

    private float coolDownDiration;
    private float nextReadyTime;
    private float coolDownTimeLeft;
    private bool canBetriggered;

    private bool initialized = false;


    public void SetAbilityUi(Ability _ability){
        if(ability == null)
        ability = _ability;

        else         
        return;
    }

    public void InitializeAbility(InventorySlot _selectedAbility, GameObject _weaponholder){
         //Having a tmp var outside of if statement for calling in the method later.
        var proj = ScriptableObject.CreateInstance<ProjectileAbility>();
        var ray = ScriptableObject.CreateInstance<RayCastAbility>();
        //Checking whether the _selectedAbility is a RayCast or a Projectile.
        if(_selectedAbility.itemObject.rb != null)//Checks if there is a RigidBody or not
        {
            //Since there is a rigidBody, it is a ProjectileAbility
                //Setting Params
                proj._sprite = _selectedAbility.itemObject.uiDisplay;
                proj.projectile = _selectedAbility.itemObject.rb;
                proj._sound = _selectedAbility.itemObject._sound;
                proj._baseCoolDown = _selectedAbility.itemObject.baseCooldown; 
                ability = proj; //After Params are set, we call Methods from the object taken from InventorySlot.
        }else{

                //Setting params
                ray._sprite = _selectedAbility.itemObject.uiDisplay;
                ray._sound = _selectedAbility.itemObject._sound;
                ray._baseCoolDown = _selectedAbility.itemObject.baseCooldown; 
                ability = ray; //After Params are set, we call Methods from the object taken from InventorySlot.

        }
        
        weaponHolder = _weaponholder;
        // myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        // myButtonImage.sprite = ability._sprite;
        // darkMask.sprite = ability._sprite;
        coolDownDiration = ability._baseCoolDown;
        ability.Initialize(weaponHolder);
        AbilityReady();
        initialized = true;
    }


    void Update(){
        if(initialized){
        bool coolDownComplete = (Time.time > nextReadyTime);
        if(coolDownComplete){
            AbilityReady();
            canBetriggered = true;
        }else{
            canBetriggered = false;
            CoolDown();
            }
        }
    }

    private void AbilityReady(){
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown(){
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = coolDownTimeLeft / coolDownDiration;
    }

    public void CheckCoolDown(){
        if(canBetriggered){
            nextReadyTime = coolDownDiration + Time.time;
        coolDownTimeLeft = coolDownDiration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
        // abilitySource.clip = ability._sound;
        // abilitySource.Play();
        ability.CastAbility();
        }
        else 
        return;
    }

}
