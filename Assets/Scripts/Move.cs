using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;
public class Move : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;
    public float stopdist = .2f;
    public bool debugStopSearching = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            float dist = Vector3.Distance(Target.position, transform.position);

            if (dist <= stopdist && debugStopSearching == false)
            {
                //Destroy(Target.gameObject);
                //gameObject.GetComponent<SearchFromColony>().StartSearch();

                string p = "Move/ Update";

                gameObject.GetComponent<SearchFromColony>().moveWaypoint(p);
                gameObject.GetComponent<SearchFromColony>().move();
            }
        }
    }

   

    public void setMoveTarget(GameObject _target, GameObject _calledBy)
    {
        Target = _target.transform;
        agent = GetComponent<NavMeshAgent>();

        agent.destination = _target.transform.position;
        //gameObject.GetComponent<StateScript>().changeState("Moving to " + _target.name);
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            //Arrived.
        }

    }

    public void checkIsMoving()
    {
        if(agent.velocity.x <= 1f && agent.velocity.y <= 1f && agent.velocity.z <= 1f)//doesnt work
        {
            Debug.Log("agent Stoped");
        }
    }
}
