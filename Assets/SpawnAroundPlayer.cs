using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAroundPlayer : MonoBehaviour
{
    public GameObject[] buff;

    int buffCount;

    int xPos, yPos, zPos;

    public GameObject player;

    public void Start(){
            StartCoroutine(SpawnBuffs());
    }

    public IEnumerator SpawnBuffs(){

        while(buffCount <= 5){
            int randBuff = Random.Range(0, buff.Length);

            int x1 = (int)player.transform.position.x + 20;
            int x2 = (int)player.transform.position.x - 20;

            int y1 = (int)player.transform.position.y + 5;
            int y2 = (int)player.transform.position.y + 10;

            int z1 = (int)player.transform.position.z + 10;
            int z2 = (int)player.transform.position.z + 15;

            xPos= Random.Range(x1, x2);
            yPos= Random.Range(y1 , y2);
            zPos = Random.Range(z1, z2);

            Instantiate(buff[randBuff], new Vector3(xPos, yPos, zPos), transform.rotation);
            yield return new WaitForSeconds(2f);
            buffCount+= 1;
        }
    }
}
