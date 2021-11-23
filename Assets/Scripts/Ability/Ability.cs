using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ItemObject
{

    [HideInInspector]
    public Sprite _sprite;
    [HideInInspector]
    public float _baseCoolDown = 1f;

    public abstract void Initialize(GameObject _obj);
    
    public abstract void CastAbility();



}
