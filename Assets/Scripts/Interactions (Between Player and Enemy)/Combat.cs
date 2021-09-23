using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class Combat : EnemyBase
{
   
    public override void Die(){

       gameObject.SetActive(false);

        foreach (var item in enemy.lootTable.equipmentThatCanBeDropped)
        {
            enemy.total += item.chanceOfDrop;
        }

        enemy.randomNumber = Random.Range(0, enemy.total);

        foreach (var weight in enemy.lootTable.equipmentThatCanBeDropped)
        {
            if(enemy.randomNumber <= weight.chanceOfDrop){
                //Award Item
                Instantiate(weight.equipmentToBeDropped, whereToSpawn.position, whereToSpawn.rotation);
            }
            else{
                enemy.randomNumber -= weight.chanceOfDrop;
            }
        }
    }
}
