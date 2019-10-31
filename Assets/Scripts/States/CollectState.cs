using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectState : BaseState
{
    private Ant _ant;
    private NavMeshAgent agent;
    private Colony homeColony;


    public CollectState(Ant ant) : base(ant.gameObject) //passes ant gameobject to base state
    {
        _ant = ant;
        agent = _ant.GetComponent<NavMeshAgent>();
        homeColony = _ant.HomeColony;

    }

    public override Type Tick()
    {
        if (_ant.Target == null)
            return typeof(WanderState);

        //DO COLLECT STUFF
        var Distance = Vector3.Distance(a: transform.position, b: _ant.Target.position);
        agent.destination = _ant.Target.position;

        if (Distance <= GameSettings.CollectDist)
        {
            //PICK UP THING
            if (_ant.PickupObject(_ant.Target.gameObject.GetComponent<Food>().pickupModel) == true)
            {
                _ant.Target.gameObject.GetComponent<Food>().PickUp(_ant.HomeColony);
                //mark food in colony marked food list
                _ant.HomeColony.AddMarkedFood(_ant.Target.gameObject.GetComponent<Food>());

                //set target home
                _ant.SetTarget(_ant.HomeColony.transform);
                
                return typeof(ReturnState);
            }
        }
        return null;
    }


}
