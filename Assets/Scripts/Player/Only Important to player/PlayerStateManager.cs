using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField]
    private VoidEvent onPause;
    [SerializeField]
    private VoidEvent onResume;
    bool changed;
    bool playerChanged;
        
    public void Awake(){
        changed = true;
    }
    public void ChangeState(){
        
        changed = !changed;

        if(!changed){
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            onPause.Raise();
        }else{
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            onResume.Raise();
        }  
    }

    public void Die(){
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }

    public void Idle(){
        
    }

    

    

    
}
