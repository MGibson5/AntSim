using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSight : MonoBehaviour
{

    public int numRays = 8;

    private void Start()
    {
    }

    private void Update()
    {
        checkSight();


    }


    public void checkSight()
    {
        bool bestPathFound = false;
        bool seeSomething = false;
        bool isClearPath = false;
        float angle = 0;
        float rayDist = 0;
        Vector2 bestVector = transform.right;
        for(int i = 0; i < numRays; i++)
        {
            for (int j =0; j < 2; j++)
            {
                if (bestPathFound == false)
                {


                    Vector3 noAngle = transform.right;
                    Quaternion spreadAngle = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
                    Vector3 newVector = spreadAngle * noAngle;

                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(transform.position, newVector, out hit, 1f))
                    {
                        Debug.DrawRay(transform.position, newVector, Color.yellow);
                        Debug.Log("Did Hit");
                        seeSomething = true;

                        if (hit.distance > rayDist && isClearPath == false)
                        {
                            rayDist = hit.distance;
                            bestVector = newVector;
                            print(rayDist);
                        }
                    }
                    else
                    {
                        bestPathFound = true;
                        isClearPath = true;
                        Debug.DrawRay(transform.position, newVector, Color.white);
                        Debug.Log("Did not Hit");
                        bestVector = newVector;
                    }


                    angle *= -1;
                }
            }
             angle += 5;


        }
        Debug.DrawRay(transform.position, bestVector, Color.green);

        print(bestVector);

    }




}
