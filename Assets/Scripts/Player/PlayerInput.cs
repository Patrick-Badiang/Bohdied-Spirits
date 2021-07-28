using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{   
    
    [SerializeField]
    private VoidEvent onAttack;
    [SerializeField]
    private VoidEvent onSave;
    [SerializeField]
    private VoidEvent onLoad;
    [SerializeField]
    private VoidEvent onInventory;    

    
    void OnMove(InputValue value){        }
    void OnJump(InputValue value){}
    void OnMouse(InputValue value){}
    void OnAttack(InputValue value){          onAttack.Raise(); }
    void OnAbility1(InputValue value){}
    void OnAbility2(InputValue value){}
    void OnAbility3(InputValue value){}
    void OnAbility4(InputValue value){}
    void OnAim(InputValue value){}
    void OnSave(InputValue value){                onSave.Raise();}
    void OnLoad(InputValue value){            onLoad.Raise();   }
    void OnInventory(InputValue value){   onInventory.Raise();   }
}
