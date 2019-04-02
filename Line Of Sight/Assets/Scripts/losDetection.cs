using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class losDetection : MonoBehaviour
{

    public GameObject[] strangers = new GameObject[10];
    public float maxAngle;
    public float maxRadius;

    private bool[] isInFOV;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
        
        for (int i = 0; i < strangers.Length; i++)
        {
            if (strangers[i] != null) {

                if(!isInFOV[i])
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.green;

                //Gizmos.DrawRay(transform.position, (strangers[i].transform.position - transform.position).normalized * maxRadius);
                Gizmos.DrawRay(transform.position, (strangers[i].transform.GetChild(0).position - transform.position).normalized * maxRadius);
                Gizmos.DrawRay(transform.position, (strangers[i].transform.GetChild(1).position - transform.position).normalized * maxRadius);
            }
            
        }
        

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);


    }

    public static bool[] inFOV(Transform checkingObject, GameObject[] targets, float maxAngle, float maxRadius)
    {
        bool[] isInFOV = new bool[10];
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {

            if (overlaps[i] != null)
            {
                for (int j = 0; j < targets.Length; j++)
                {
                    if (targets[j] != null && overlaps[i].transform == targets[j].transform)
                    {

                        Vector3 directionBetween = (targets[j].transform.position - checkingObject.position).normalized;
                        directionBetween.y *= 0;

                        float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                        if (angle <= maxAngle)
                        {

                            Ray rayRight = new Ray(checkingObject.position, targets[j].transform.GetChild(0).position - checkingObject.position);
                            Ray rayLeft = new Ray(checkingObject.position, targets[j].transform.GetChild(1).position - checkingObject.position);
                            RaycastHit hit;

                            if (Physics.Raycast(rayLeft, out hit, maxRadius))
                            {
                                if (hit.transform == targets[j].transform)
                                    isInFOV[j] = true;

                            }

                            if (Physics.Raycast(rayRight, out hit, maxRadius))
                            {
                                if (hit.transform == targets[j].transform)
                                    isInFOV[j] = true;

                            }


                        }


                    }
                }
            }

        }

        return isInFOV;
    }

    private void Update()
    {

        isInFOV = inFOV(transform, strangers, maxAngle, maxRadius);

    }

}

// script from AJTech "(04) Stealth Game - Field of View (Unity, C#)"