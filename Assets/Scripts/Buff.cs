using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private BuffSO buff;

    public int _speedBonus;
    
    [SerializeField]
    private FloatVariable buffCount;

    void Awake(){
        _speedBonus = buff.speedBonus;
    }

    public void HitBuff(PlayerController _player){
        _player.BuffIsHit(_speedBonus);
        buffCount.ApplyChange(-1);
        gameObject.SetActive(false);
    }

}
