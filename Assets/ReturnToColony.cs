using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ReturnToColony : MonoBehaviour
{

    private bool returningToColony = false;
    private GameObject calledBy;
    // Update is called once per frame
    void Update()
    {
        if (returningToColony)
        {
            float dist = Vector3.Distance(gameObject.GetComponent<CreatureStats>().getHomeColony().transform.position, transform.position);
            if (dist <= 1f)
            {
                //REACHED COLONY

            }
        }
    }

    public void returnToColony(GameObject _calledBy)
    {
        calledBy = _calledBy;
        NavMeshAgent agent;

        agent = GetComponent<NavMeshAgent>();

        Colony target = gameObject.GetComponent<CreatureStats>().getHomeColony();
        agent.destination = target.transform.position;
        returningToColony = true;
        //gameObject.GetComponent<SearchFromColony>().destroyCurrentWaypoint();
        string p = "GrabObject/ returnObjectToColony";

        gameObject.GetComponent<SearchFromColony>().moveWaypoint(p);
    }
        

}
