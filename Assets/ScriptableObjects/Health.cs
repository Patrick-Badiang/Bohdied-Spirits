using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Reference", menuName = ("ScriptableObject/Health/New Health"))]
public class Health : ScriptableObject
{
    public float hp;

    public VoidEvent onDeath;

    // public void UpdateHealth(float damage){
        
    //     float takendamage = damage * (1 - GetValue(0));
    //     takendamage = Mathf.Clamp(takendamage, 0, int.MaxValue);
        
    //     hp  -= takendamage;

    //     Debug.Log(hp);

    //     if(hp <= 0) onDeath.Raise();
        
    
    // }

}
