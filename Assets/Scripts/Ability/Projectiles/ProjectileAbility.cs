using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Abilities/Projectiles")]

public class ProjectileAbility : Ability
{
    public AbilityCoolDown abilityCoolDown;
    //Make a scriptable object that takes in variables of abilitycooldown.
    //For example: a scriptable object named "SetParameters" that takes has the correct given variables
    //Then it is then used in the StaticINterface class in order to set
    //the slot variables to the "SetParameters" vairables.
    public float forceValue = 500f;
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
