using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public int numRays = 8;
    public float sightDist = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckSight();
    }

    public void CheckSight()
    {
        float angle = 0;
        Vector3 noAngle = transform.forward;

        for (int i = 0; i < numRays; i++)
        {
            for (int j = 0; j < 2; j++)
            {

                Quaternion spreadAngle = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
                Vector3 newVector = spreadAngle * noAngle;

                RaycastHit hit;

                if (Physics.Raycast(transform.position, newVector, out hit, sightDist))
                {
                    if (hit.collider.GetComponent<Seen>())
                    {
                        hit.collider.GetComponent<Seen>().ThisObjectSeen(this.gameObject);
                    }
                    Debug.DrawRay(transform.position, newVector * sightDist, Color.yellow);
                }
                else
                {
                    Debug.DrawRay(transform.position, newVector * sightDist, Color.white);

                }

                angle *= -1; //invert angle

            }
            angle += 5; //add 5 degree to angle

        }
    }
}
