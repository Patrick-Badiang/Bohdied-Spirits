using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedVFX : MonoBehaviour
{
    [Header("Time Of Animation")]
    [Range(0.0f,2.0f)]
    public float animTime;

    void Awake(){
        StartCoroutine(Death());
    }

    IEnumerator Death(){
        Debug.Log("Starting Anim");
        yield return new WaitForSeconds(animTime);

        Debug.Log("Done Anime, going inactive");
        gameObject.SetActive(false);

    }
}
