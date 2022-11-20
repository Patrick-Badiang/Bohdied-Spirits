using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Camera cam;
    Vector3 currentEulerAngles;

    public float speed;

    
    public void PlayClicked(){
        SceneManager.LoadScene("MainGame");
    }

    void Update(){
        currentEulerAngles += new Vector3(0, speed, 0) * Time.deltaTime;

        //apply the change to the gameObject
        cam.transform.eulerAngles = currentEulerAngles;
        
    }

}
