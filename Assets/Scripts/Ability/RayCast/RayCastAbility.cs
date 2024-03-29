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

    private RayCastAction cast;

    public override void CastAbility()
    {
        cast.Launch();
    }
    public override void Initialize(GameObject _obj)
    {
        cast = _obj.GetComponent<RayCastAction>();
    }

}
