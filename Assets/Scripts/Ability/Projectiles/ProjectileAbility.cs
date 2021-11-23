using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Abilities/Projectiles")]

public class ProjectileAbility : Ability
{

    public float forceValue = 500f;
    [HideInInspector]
    public Rigidbody projectile;

    private ProjectileShootTriggerable launcher;

    public override void Initialize(GameObject _obj)
    {
        launcher = _obj.GetComponent<ProjectileShootTriggerable>();
        launcher.forceValue = forceValue;
        launcher.rb = projectile;
    }

    public override void CastAbility()
    {
        launcher.Launch();
    }
}
