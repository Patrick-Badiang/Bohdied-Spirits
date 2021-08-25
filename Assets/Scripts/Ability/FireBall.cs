using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Abilities/RayCastAbilities")]
public class RayCastAbility : Ability
{
    public int _damage;
    public float _range;
    public float _hitForce;
    public Color _color = Color.white;

    public override void CastAbility()
    {
    }
    public override void Initialize(GameObject _obj)
    {
    }

}
