using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MarkFood : MonoBehaviour
{
    private GameObject Target;
    NavMeshAgent agent;
    bool Traveling = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Traveling)
            checkArrived();
    }

    public void setFoodTarget(GameObject _target)
    {
        //Debug.Log("CALLED setFoodTarget() in MARK FOOD");

        Traveling = true;
        Target = _target;
        //gameObject.GetComponent<Move>().debugStopSearching = true;
        //gameObject.GetComponent<Move>().setMoveTarget(_target);

        agent = GetComponent<NavMeshAgent>();
        agent.destination = _target.transform.position;

        
    }

    private void checkArrived()
    {
        if (Target != null)
        {
            float dist = agent.remainingDistance;
            if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= .5f)
            {
                //Arrived.
                Traveling = false;

                setFoodMarked();

                pickUpFood();

            }
        }
        else
        {
            //INSERT START OF CYCLE: FOR NOW USING SEARCH FROM COLONY
            //gameObject.GetComponent<SearchFromColony>().destroyCurrentWaypoint();

            // gameObject.GetComponent<SearchFromColony>().StartSearch();
            string p = "MarkFood/ CheckArrived";
            Traveling = false;
            gameObject.GetComponent<SearchFromColony>().moveWaypoint(p);
            gameObject.GetComponent<SearchFromColony>().move();
        }
    }

    private void setFoodMarked()
    {
        if (Target != null)
        {
            print(Target.name);
            if (Target.GetComponent<Food_Marked>())
            { Debug.Log(Target.name + " TARGET HAS FOOD MARKED SCRIPT"); }

            Target.GetComponent<Food_Marked>().setMarked(gameObject.GetComponent<CreatureStats>().getHomeColony());
        }
        else { Debug.LogAssertion("TARGET DOES NOT EXIST FOR " + gameObject.name); }
    }

    private void pickUpFood()
    {
        //Debug.Log("CALLED PUCKUPFOOD() in MARK FOOD");
        if(Target != null)
            gameObject.GetComponent<GrabObject>().pickupObject(Target, true);
    }

}
