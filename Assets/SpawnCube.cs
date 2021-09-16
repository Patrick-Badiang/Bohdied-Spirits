using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public PlayerCombat player;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, transform.rotation);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
