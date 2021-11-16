using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ItemObject
{

    public string _name = "New Ability";
    public Sprite _sprite;
    public float _baseCoolDown = 1f;

    public abstract void Initialize(GameObject _obj);
    
    public abstract void CastAbility();



}
