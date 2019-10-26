using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GrabObject : MonoBehaviour
{
    public Transform carryObjectSpawnTransform;
    private GameObject carriedObject;
    bool returningToColony = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (returningToColony)
        {
            float dist = Vector3.Distance(gameObject.GetComponent<CreatureStats>().getHomeColony().transform.position, transform.position);
            if (dist <= 1f)
            {
                //Arrived.
                //Debug.Log("FOOD RETURNED TO COLONY");
                gameObject.GetComponent<CreatureStats>().getHomeColony().AddFood(1);
                Destroy(carriedObject);

                //gameObject.GetComponent<SearchFromColony>().StartSearch();

                string p = "GrabObject/ Update";

                gameObject.GetComponent<SearchFromColony>().moveWaypoint(p);
                gameObject.GetComponent<SearchFromColony>().move();


                returningToColony = false;

            }
        }
    }

    public void pickupObject(GameObject _object, bool returnToColony)
    {
        if (_object != null)
        {
            if (getCarryingObject() == false)
            {
                if (_object.GetComponent<Carryable>())
                {
                    if (carriedObject == null)
                    {
                        carriedObject = Instantiate(_object.GetComponent<Carryable>().getCarryObject(), carryObjectSpawnTransform);
                        gameObject.GetComponent<StateScript>().changeState("Picking Up Object");

                        if (_object.GetComponent<FoodStats>())
                        {
                            _object.GetComponent<FoodStats>().changeSize(-1);
                        }
                    }

                    if (returnToColony)
                    {
                        returnObjectToColony();
                    }
                }
            }
        } else
        {
            //INSERT START OF CYCLE: FOR NOW USING SEARCH FROM COLONY
            string p = "GrabObject/ pickupObject";

            //gameObject.GetComponent<SearchFromColony>().StartSearch();
            gameObject.GetComponent<SearchFromColony>().moveWaypoint(p);
            gameObject.GetComponent<SearchFromColony>().move();

        }
    }

    private void returnObjectToColony()
    {
        Debug.Log("RETURNING");
        NavMeshAgent agent;

        agent = GetComponent<NavMeshAgent>();

        Colony target = gameObject.GetComponent<CreatureStats>().getHomeColony();
        agent.destination = target.transform.position;
        returningToColony = true;
        //gameObject.GetComponent<SearchFromColony>().destroyCurrentWaypoint();
        string p = "GrabObject/ returnObjectToColony";

        gameObject.GetComponent<SearchFromColony>().moveWaypoint(p);
    }

    public bool getCarryingObject()
    {
        if (carriedObject == null)
            return false;
        else
            return true;
    }
}
