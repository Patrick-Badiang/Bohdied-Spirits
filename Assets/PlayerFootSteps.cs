using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : PlayerSoundEffects
{

    void OnTriggerEnter(){
        footStep.Play();
    }

}
