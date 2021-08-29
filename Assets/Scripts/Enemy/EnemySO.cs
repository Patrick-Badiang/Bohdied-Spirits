using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/New Failed-Spirit")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Model")]
    public GameObject enemyModel;

    [Header("Loot Table")]
    public RandomLoot lootTable;

    [HideInInspector]
    public int total;
    [HideInInspector]
    public int randomNumber;

    [Header("Stats")]
    public float health;
    public int damage;
    public int armor;
    public int attackSpeed;


    [Header("Animations")]
    public float deathDelay;
    public float attackDuration;

    [Header("Elements")]
    public float vulnerableDamage;
    public ElementType elementType;
    public ElementType[] vulnerableElementType;
}
