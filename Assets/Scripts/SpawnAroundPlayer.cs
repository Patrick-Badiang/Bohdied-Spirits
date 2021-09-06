using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAroundPlayer : MonoBehaviour
{
    public BuffSO[] buff;

    [SerializeField]
    private FloatVariable buffCount;

    int xPos, yPos, zPos;

    public GameObject player;

    public void Start(){
            StartCoroutine(SpawnBuffs());
    }

    public IEnumerator SpawnBuffs(){

        while(buffCount.Value < 10){
            Vector3 playerPos = player.transform.position;
            Vector3 playerDirection = player.transform.forward;
            Quaternion playerRotation = player.transform.rotation;

            int randBuff = Random.Range(0, buff.Length);

            xPos= Random.Range(20,-20);
            yPos= Random.Range(1,5);
            zPos = Random.Range(40,50);

            Vector3 spawnPos = playerPos + playerDirection *zPos + player.transform.up * yPos + player.transform.right * xPos;

            GameObject clone = Instantiate(buff[randBuff].model, spawnPos, playerRotation);
            yield return new WaitForSeconds(2f);
            
            if(clone != null){ StartCoroutine(Countdown(clone)); }
            
            buffCount.ApplyChange(1);
        }
    }

    IEnumerator Countdown(GameObject _object){
        yield return new WaitForSeconds(4);
        _object.SetActive(false);
        buffCount.ApplyChange(-1);
    }
}
