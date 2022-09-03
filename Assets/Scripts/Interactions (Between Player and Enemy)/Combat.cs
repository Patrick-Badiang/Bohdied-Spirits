using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class Combat : EnemyBase
{
   [Header("Loot Table")]
    public RandomLoot lootTable;

    [Header("Death Animation")]
    public GameObject smokePuff;

    [HideInInspector]
    public int total;
    [HideInInspector]
    public int randomNumber;

    public override void Die(){
        GameObject smoke = Instantiate(smokePuff, transform.position, transform.rotation);

       gameObject.SetActive(false);

        foreach (var item in lootTable.equipmentThatCanBeDropped)
        {
            total += item.chanceOfDrop;
        }

        randomNumber = Random.Range(0, total);

        foreach (var weight in lootTable.equipmentThatCanBeDropped)
        {
            if(randomNumber <= weight.chanceOfDrop){
                //Award Item
                Instantiate(weight.equipmentToBeDropped, whereToSpawn.position, whereToSpawn.rotation);
            }
            else{
                randomNumber -= weight.chanceOfDrop;
            }
        }
    }
}
