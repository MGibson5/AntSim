using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderState : BaseState
{
    private Vector3? destination;
    private float stopDistance = 1.0f;
    private Ant _ant;
    private float wanderRadius;
    //private Colony homeColony;
    private Quaternion stepAngle = Quaternion.AngleAxis(5.0f, Vector3.up);
    private NavMeshAgent agent;

    private Vector3 direction;
    private Quaternion startingAngle = Quaternion.AngleAxis(-60.0f, Vector3.up);

    public ColonyManager ColonyManager;
    //constructor
    public WanderState(Ant ant) : base(ant.gameObject) //passes ant gameobject to base state
    {
        _ant = ant;
        ColonyManager = _ant.Colony_Manager;
        //homeColony = _ant.HomeColony;
        agent = _ant.GetComponent<NavMeshAgent>();

    }

    //works like Update()
    public override Type Tick()
    {
        //Check if can see something and set to target and return relevant state
        /*var enemyTarget = CheckForEnemy();
        if(enemyTarget != null)
        {

        }*/
        if (_ant.hunger <= _ant.maxHunger / 3) //CAN SET 3 TO A RETURN FOR FOOR SENSITIVITY VARIABLE THAT PLAYER CAN ADJUST
        {
            //return To Colony for food
            destination = null;

            return (typeof(ReturnState));

        }
        //check if underattack
        if(_ant.Attacking == true && _ant.Target != null)
        {
            destination = null;
            return (typeof(AttackState));
        }
        //CHECK ENEMIES (possibly do this along with food but move marked foods inbetween so marked foods is checked before finding new foods
        var target = CheckForTarget();

        if (target != null && target.GetComponent<Ant>())
        {
            _ant.SetTarget(target);
            destination = null;
            return (typeof(AttackState));
        }
        //CHECK MARKED FOODS
        if (_ant.HomeColony.MarkedFoods.Count > 0 && _ant.HomeColony.MarkedFoods[0] != null )
        {
            _ant.SetTarget(_ant.HomeColony.MarkedFoods[0].transform);
            destination = null;
            return (typeof(CollectState));
        }


        if(target != null && target.GetComponent<Food>())
        {
            _ant.SetTarget(target);
            destination = null;
            return (typeof(CollectState));
        }
        //Possibly add hungry state which makes ant return home for food?

        //if no target wander
        if (destination.HasValue == false || Vector3.Distance(transform.position, destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }
        return null;
    }

    private Transform CheckForTarget()
    {
        /*RaycastHit2D hit2D;
        var angle = transform.rotation * startingAngle;
        direction = angle * Vector3.up;
        var pos = transform.position;
        for (int i = 0; i < 24; i++)
        {
            Debug.DrawRay(pos, direction * GameSettings.SightDist, Color.white);

            hit2D = Physics2D.Raycast(pos, direction, GameSettings.SightDist);
            if (hit2D.collider != null)
            {
                var spotted = hit2D.collider.GetComponent<Food>();
                Debug.Log(spotted);
                return spotted.transform;
            }
            direction = stepAngle * direction;
            Debug.Log(direction);
        }*/
        float angle = 0;
        Vector3 noAngle = transform.forward;

        for (int i = 0; i < _ant.antSettings.sightRayAmount; i++)
        {
            for (int j = 0; j < 2; j++)
            {

                Quaternion spreadAngle = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));
                Vector3 newVector = spreadAngle * noAngle;

                RaycastHit hit;

                if (Physics.Raycast(transform.position, newVector, out hit, (_ant.antSettings.sightDist + 1)))
                {
                    if (hit.collider.GetComponent<Ant>())
                    {
                        Debug.Log(ColonyManager.GetComponent<ColonyManager>().GetOpinion(_ant.GetComponent<Ant>().HomeColony.ID, hit.collider.GetComponent<Ant>().HomeColony.ID));
                        if (hit.collider.GetComponent<Ant>().HomeColony.ID != _ant.GetComponent<Ant>().HomeColony.ID)
                        {
                            if (ColonyManager.GetComponent<ColonyManager>().GetOpinion(_ant.GetComponent<Ant>().HomeColony.ID, hit.collider.GetComponent<Ant>().HomeColony.ID) <= 25)
                            {
                                var spotted = hit.collider.GetComponent<Ant>();

                                return spotted.transform;
                            }
                        }
                    }
                    else if (hit.collider.GetComponent<Food>())
                    {
                        var spotted = hit.collider.GetComponent<Food>();

                        return spotted.transform;
                    }
                    
                    Debug.DrawRay(transform.position, newVector * (_ant.antSettings.sightDist + 1), Color.yellow);
                }
                else
                {
                    Debug.DrawRay(transform.position, newVector * (_ant.antSettings.sightDist + 1), Color.white);

                }

                angle *= -1; //invert angle

            }
            angle += 5; //add 5 degree to angle

        }
        return null;
    }

    void FindRandomDestination()
    {
        wanderRadius = 5 * _ant.HomeColony.GetColonySize();
        
        float x = 0;
        float y = 0;
        int r = Random.Range(0, 2);

        if (r == 1)
        {
            x = Random.Range(-5, wanderRadius);
        }
        else
        {
            x = Random.Range(-5, (wanderRadius * -1));
        }

        r = r = Random.Range(0, 2);
        if (r == 1)
        {
            y = Random.Range(-5, wanderRadius);
        }
        else
        {
            y = Random.Range(-5, (wanderRadius * -1));
        }

        Vector3 newSearchPos = _ant.HomeColony.transform.position + new Vector3(x, 0, y);
        agent.destination = newSearchPos;
        destination = newSearchPos;

        Debug.DrawRay(newSearchPos, transform.up , Color.blue, 1);

        //gameObject.GetComponent<StateScript>().changeState("Patrolling");

        //currentSearchObject.transform.position = newSearchPos;
    }

}
