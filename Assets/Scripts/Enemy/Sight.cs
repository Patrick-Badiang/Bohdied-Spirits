using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;
    public LayerMask whatIsPlayer;
    public LayerMask obstaclesLayers;

    public Collider detectedObject;
    
    static Collider[] colliders = new Collider[100];

    void Update()
    {
        int detectedAmount= Physics.OverlapBoxNonAlloc(transform.position, transform.localScale/2, colliders,Quaternion.identity, whatIsPlayer);

        detectedObject = null;
        for (int i = 0; i < detectedAmount; i++)
        {
            Collider collider = colliders[i];
            
            Vector3 directionToCollider = Vector3.Normalize(collider.bounds.center - transform.position);
            
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);
            
            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstaclesLayers))
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedObject = collider;
                    break;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }    
    }

     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale/2);

        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * distance);
        
        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);
    }
}
