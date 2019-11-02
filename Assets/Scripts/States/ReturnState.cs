using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReturnState : BaseState
{
    private Ant _ant;
    private NavMeshAgent agent;
    private bool moving = false;
    public ReturnState(Ant ant) : base(ant.gameObject) //passes ant gameobject to base state
    {
        _ant = ant;
        agent = _ant.GetComponent<NavMeshAgent>();
    }
    public override Type Tick()
    {
        if(moving == false)
            agent.destination = _ant.HomeColony.transform.position;
        float dist = Vector3.Distance(_ant.transform.position, _ant.HomeColony.transform.position);
        if (dist  <= _ant.antSettings.collectDist)
        {
            _ant.DepositObject();
            _ant.SetTarget(null);
            _ant.GainFood();
            return typeof(WanderState);
        }
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
