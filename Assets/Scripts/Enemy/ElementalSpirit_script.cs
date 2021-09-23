using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSpirit_script : EnemyBase
{
    [SerializeField] private VoidEvent onTriggerChoiceScreen;
    Ability _ability;
    public override void Die(){

       gameObject.SetActive(false);

        onTriggerChoiceScreen.Raise();
    
    }
}
