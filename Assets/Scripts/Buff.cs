using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Speed Buff", menuName = "ScriptableObject/Spawnables/New Speed Buff")]
public class Buff : ScriptableObject
{
    public GameObject model;
    public int speedBonus;
}
