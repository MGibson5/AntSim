﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public Transform Target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();

    public Colony HomeColony { get; set; }

    [SerializeField] private Transform PickUpObjectTransform;

    private bool carryingObject = false;
    private GameObject carriedObject = null;

    public int hunger { get; private set; }
    public int maxHunger { get; private set; } = 10;

    private int hungerTick;
    [SerializeField] private int hungerTickMax = 50;

    private void Awake()
    {
        HomeColony = FindObjectOfType<Colony>(); //TEMP FIND COLONY (Should be set when ant spawned)
        InitialiseStateMachine();
        hunger = maxHunger;
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        hungerTick++;
        if (hungerTick >= hungerTickMax)
        {
            hungerTick = 0;
            hunger -= 1;
            if (hunger <= maxHunger / 10)
            {
                //return To Colony for food
            }
            if (hunger <= 0)
            {
                if (gameObject != null)
                    Destroy(gameObject);
            }
        }
    }

    private void InitialiseStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(ant: this) }, //reson for like this is to reuse the same State. We also pass in the ant so its for a specific ant. Add all states other bellow
            {typeof(ReturnState), new ReturnState(ant: this) },
            {typeof(CollectState), new CollectState(ant: this)}

        };

        GetComponent<StateMachine>().SetStates(states);

    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public bool PickupObject(GameObject _object)
    {
        if (carryingObject == false)
        {
            carriedObject = Instantiate(_object, PickUpObjectTransform, false);
            carryingObject = true;
        }
        return carryingObject;

    }

    public void DepositObject()
    {
        Debug.Log("DEPOSIT");
        if (carryingObject == true)
        {
            Destroy(carriedObject);
            HomeColony.ChangeColonyFood(1);
            carryingObject = false;
        }

    }

    public void GainFood()
    {
        hunger = maxHunger;
    }
}