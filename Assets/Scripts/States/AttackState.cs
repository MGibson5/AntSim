using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private Ant _ant;
    private NavMeshAgent agent;

    public AttackState(Ant ant) : base(ant.gameObject) //passes ant gameobject to base state
    {
        _ant = ant;
        agent = _ant.GetComponent<NavMeshAgent>();
    }
    public override Type Tick()
    {
        if (_ant.Target == null)
            return typeof(WanderState);

        var Distance = Vector3.Distance(a: transform.position, b: _ant.Target.position);
        agent.destination = _ant.Target.position;

        if (Distance <= GameSettings.CollectDist)
        {
            if (_ant.Target)
            {
                
                if (_ant.Target.gameObject.GetComponent<Ant>())
                {
                    Ant _enemy = _ant.Target.gameObject.GetComponent<Ant>();
                    _ant.Attack(_enemy);
                }
                //set target home
                // _ant.SetTarget(_ant.HomeColony.transform);

                //return typeof(ReturnState);
            }
            else
            {
                return typeof(WanderState);
            }
            
        }

        if (_ant.Target.gameObject.active == false)
        { return typeof(WanderState); }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
