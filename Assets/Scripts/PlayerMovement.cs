using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            transform.Translate(0, 0, -speed * Time.deltaTime);

        }if(Input.GetKeyDown(KeyCode.A)){

            transform.Translate( speed * Time.deltaTime,0,0 );

        }if(Input.GetKeyDown(KeyCode.D)){
            transform.Translate(-speed * Time.deltaTime, 0,0 );
        }
    }
}
