using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    float  _min, _max, _variable;
    
    public Image fill;


    public void SetMaxHealth(float health, float min){

        fill.fillAmount = Mathf.Clamp01(
                Mathf.InverseLerp(min, health, health));
        
        _max = health;
        _min = min;
    }

    public void SetHealth(float health){
        fill.fillAmount = Mathf.Clamp01(
            Mathf.InverseLerp(_min, _max, health));
    }
}
