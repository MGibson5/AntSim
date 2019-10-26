using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchFromColony : MonoBehaviour
{
    public Colony homeColony;
    private GameObject currentSearchObject;
    private Move moveScript;
    public GameObject TargetWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = gameObject.GetComponent<Move>();
        //StartSearch();
        homeColony = gameObject.GetComponent<CreatureStats>().getHomeColony();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSearch()
    {
        moveScript = gameObject.GetComponent<Move>();
        homeColony = gameObject.GetComponent<CreatureStats>().getHomeColony();

        if (homeColony.checkForMarkedFood() == null)
        {
            if (!currentSearchObject)
            {
                if (homeColony == null)
                {
                    homeColony = gameObject.GetComponent<CreatureStats>().getHomeColony();
                }
                //Debug.Log("HOME COLONY " + homeColony.name);
                float searchRadius = 5 * homeColony.GetComponent<Colony>().getColonySize();
                //Debug.Log("Search radius = " + searchRadius);
                float x = 0;
                float y = 0;
                int r = Random.Range(0, 2);

                if (r == 1)
                {
                    x = Random.Range(-5, searchRadius);
                }
                else
                {
                    x = Random.Range(-5, (searchRadius * -1));
                }

                r = r = Random.Range(0, 2);
                if (r == 1)
                {
                    y = Random.Range(-5, searchRadius);
                }
                else
                {
                    y = Random.Range(-5, (searchRadius * -1));
                }

                Vector3 newSearchPos = homeColony.transform.position + new Vector3(x, 0, y);

                gameObject.GetComponent<StateScript>().changeState("Patroling");


                currentSearchObject = Instantiate(TargetWaypoint, newSearchPos, Quaternion.identity);
                moveScript.setMoveTarget(currentSearchObject, this.gameObject);
            }
            else
            {
                //destroyCurrentWaypoint();


                gameObject.GetComponent<SearchFromColony>().move();
            }
        }
        else
        {
            if (homeColony.checkForMarkedFood() != null)
            {
                currentSearchObject = Instantiate(TargetWaypoint, homeColony.checkForMarkedFood().transform.position, Quaternion.identity);
                moveScript.setMoveTarget(currentSearchObject, this.gameObject);
            }
        }
    }

    public void destroyCurrentWaypoint()
    {
        if (currentSearchObject) { }
            //Destroy(currentSearchObject);
    }

    public void moveWaypoint(string requestedBy)
    {
        homeColony = gameObject.GetComponent<CreatureStats>().getHomeColony();

        if (homeColony.checkForMarkedFood() == null)
        {
            //REPLACES DESTROY CURRENT WAYPOINT AND SIMPLY MOVES IT INSTEAD
            float searchRadius = 5 * homeColony.getColonySize();
            //Debug.Log("Search radius = " + searchRadius);
            //Debug.Log("Requested BY: " + requestedBy);
            float x = 0;
            float y = 0;
            int r = Random.Range(0, 2);

            if (r == 1)
            {
                x = Random.Range(-5, searchRadius);
            }
            else
            {
                x = Random.Range(-5, (searchRadius * -1));
            }

            r = r = Random.Range(0, 2);
            if (r == 1)
            {
                y = Random.Range(-5, searchRadius);
            }
            else
            {
                y = Random.Range(-5, (searchRadius * -1));
            }

            Vector3 newSearchPos = homeColony.transform.position + new Vector3(x, 0, y);
            gameObject.GetComponent<StateScript>().changeState("Patrolling");

            currentSearchObject.transform.position = newSearchPos;
        }
        else
        {
            if (homeColony.checkForMarkedFood() != null)
            {
                currentSearchObject.transform.position = homeColony.checkForMarkedFood().transform.position;
                //moveScript.setMoveTarget(currentSearchObject, this.gameObject);
            }
        }
    }

    public void move()
    {
        Debug.Log("MOVE");
        moveScript.setMoveTarget(currentSearchObject, this.gameObject);
    }
}
