using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMove : MonoBehaviour
{
    public Transform Target;
    public float speed = .5f;
    public float rotateSpeed = 5f;
    public float stopDistance = .1f;
    public int numRays = 8;
    bool isClearPath;
    Vector2 bestVector;
    bool seeSomething = false;
    public LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        checkSight();
        move();
    }

    public void move()
    {
        if(Target != null)
        {
            //get distance to target. Do whilst distance is greater than stop distance
            if (Vector2.Distance(Target.position, transform.position) >= stopDistance)
            {
               if (seeSomething == false)
                {
                    //get and set direction to target
                    Vector2 direction = (Target.position - transform.position).normalized;
                    transform.Translate((direction * speed) * Time.deltaTime, Space.World);

                    //Rotate Ant
                    Vector3 vectorToTarget = Target.position - transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
                } else
                {
                    //get and set direction to target
                    Vector2 direction = (Target.position - transform.position).normalized;
                    transform.Translate((bestVector * speed) * Time.deltaTime, Space.World);

                    //Rotate Ant
                    Vector3 vectorToTarget = Target.position - transform.position;
                    float angle = Mathf.Atan2(bestVector.y, bestVector.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
                }
            }


        }
    }

    public void SetTarget(Transform _target)
    {

    }

    public void checkSight()
    {
        bestVector = (Target.position - transform.position);

        bool bestPathFound = false;
        seeSomething = false;
        isClearPath = false;
        float angle = 0;
        float rayDist = 0;
        for (int i = 0; i < numRays; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (bestPathFound == false)
                {


                    Vector3 noAngle = transform.right;
                    Quaternion spreadAngle = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
                    Vector3 newVector = spreadAngle * noAngle;

                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(transform.position, newVector, out hit, 2f, layerMask))
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
