using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public class Combat : MonoBehaviour
{
    public EnemySO enemy;
    public Transform whereToSpawn;


    float health;


    Health _healthUi;
    bool vulnerable;
    float lastAttackTime;

    public void Awake(){
        GameObject visuals = Instantiate(enemy.enemyModel);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;

        health = enemy.health;
        _healthUi = GetComponent<Health>();
        _healthUi.SetMaxHealth(health, 0);
    }


    public void DoDamage(Collider other){
        StartCoroutine(AttackAnimation());
        PlayerStats healthScript = other.GetComponent<PlayerStats>();
        
        if(healthScript != null){
            if(Time.timeScale > 0){
            var timeSinceLastShoot = Time.time - lastAttackTime;
            if(timeSinceLastShoot < enemy.attackSpeed)
                return;
            
            lastAttackTime = Time.time;
            healthScript.TakeDamage(enemy.damage);
            }
        }
    }

    IEnumerator AttackAnimation(){
        // Make animator parameter set to true

        yield return new WaitForSeconds(enemy.attackDuration);
    }

    public void TakeDamage(float damage, ElementType passed_elementType){

        damage -= enemy.armor;
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //Makes sure that the damage never reaches below zero

        if(CheckElement(passed_elementType)){ damage *= enemy.vulnerableDamage;}
        

        if(health > 0)
        health -= damage;
        _healthUi.SetHealth(health);
        
        
        if(health <= 0){
            Die();
        }
    }

    bool CheckElement(ElementType passed_elementType){
        for(int i = 0; i < enemy.vulnerableElementType.Length; i++){
            if(enemy.vulnerableElementType[i] == passed_elementType){
                return true;
                
            }
            return false;      
        }
        return false;
    }

    void Die(){

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
