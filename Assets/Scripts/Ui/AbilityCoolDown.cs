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

    // void Start(){
    //     InitializeAbility(ability, weaponHolder);
    // }

    

    public void SetAbilityUi(Ability _ability){
        if(ability == null)
        ability = _ability;

        else 
        Debug.Log("Has ability already");
        return;
    }

    public void InitializeAbility(Ability _selectedAbility, GameObject _weaponholder){
        ability = _selectedAbility;
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
            Buttontriggered();
        }
        else 
        return;
    }
    public void Buttontriggered(){
        nextReadyTime = coolDownDiration + Time.time;
        coolDownTimeLeft = coolDownDiration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
        // abilitySource.clip = ability._sound;
        // abilitySource.Play();
        ability.CastAbility();
    }

}
