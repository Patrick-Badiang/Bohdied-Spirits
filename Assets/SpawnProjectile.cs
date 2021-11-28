using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnProjectile : MonoBehaviour
{

    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    [SerializeField] private InputActionReference mouseControl;


    private GameObject effectToSpawn;

    private void OnEnable(){
        mouseControl.action.Enable();
    }

    private void OnDisable(){
        mouseControl.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        // if(mouseControl.action.triggered){
        //     StartVFX();
        // } 
    }

    public void StartVFX(){

        GameObject vfx;
        if(firePoint != null){
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
        }else{
            Debug.Log("No FirePoint");
        }
    }
}
