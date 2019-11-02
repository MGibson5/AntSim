using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class Ant : MonoBehaviour
{
    public Transform Target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();

    public Colony HomeColony { get; set; }
    public Colony debugColony;

    [SerializeField] private Transform PickUpObjectTransform;

    private bool carryingObject = false;
    private GameObject carriedObject = null;

    public int hunger { get; private set; }
    public int debugHunger;

    public int maxHunger { get; private set; } = 10;


    public int hp { get; private set; }
    [SerializeField] private int debugHP;
    public int maxHP { get; private set; } = 10;

    private int hungerTick;
    [SerializeField] private int hungerTickMax = 50;

    private int attackTick;
    [SerializeField] private int attackTickMax = 10;

    private NavMeshAgent agent;
    public Color AntColour;

    public int Strength = 1;
    public int Defence = 1;

    public ColonyManager Colony_Manager;

    public bool Attacking = false;
    private Ant enemy;

    public AntSettings antSettings;
    private void Awake()
    {
        if (HomeColony == null) {
            HomeColony = FindObjectOfType<Colony>(); //TEMP FIND COLONY (Should be set when ant spawned)

        }
        antSettings = HomeColony.gameObject.GetComponent<AntSettings>();
        Colony_Manager = FindObjectOfType<ColonyManager>();
        InitialiseStateMachine();
        hunger = maxHunger;
        hp = maxHP;
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
        agent = gameObject.GetComponent<NavMeshAgent>();

        gameObject.GetComponent<Renderer>().material.SetColor("_Color", AntColour);
    }

    private void Update()
    {
        debugHunger = hunger;
        debugColony = HomeColony;
        debugHP = hp;
    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        ChangeSpeed();

        hungerTick++;
        attackTick++;

        if (hungerTick >= hungerTickMax)
        {
            hungerTick = 0;
            hunger -= 1;
            if (hunger <= maxHunger / 2)
            {
                //return To Colony for food
               // if(gameObject!= null)
                    //GetComponent<StateMachine>().SwitchStateManualy(typeof(ReturnState));

            }
            if (hunger <= 0)
            {
                if (gameObject != null)
                {
                    gameObject.SetActive(false);
                    //Destroy(gameObject);
                }
            }
        }
        if(attackTick >= attackTickMax)
        {
            if (Attacking && enemy != null)
            {
                if (UnityEngine.Random.Range(0, 100) * Strength >= enemy.Defence)
                {
                    enemy.hp -= 1;
                    if (enemy.hp <= 0)
                    {
                        Debug.Log(enemy.gameObject.name + " has been killed by " + gameObject.name);
                        Attacking = false;
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void InitialiseStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(ant: this) }, //reson for like this is to reuse the same State. We also pass in the ant so its for a specific ant. Add all states other bellow
            {typeof(ReturnState), new ReturnState(ant: this) },
            {typeof(CollectState), new CollectState(ant: this)},
            {typeof(AttackState), new AttackState(ant: this)}

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

    public void Attack(Ant _enemy)
    {
        enemy = _enemy;
        Attacking = true;
        
        _enemy.UnderAttack(this);
    }



    public void DepositObject()
    {
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

    public void ChangeSpeed()
    {
        if(agent)
            agent.speed = antSettings.antSpeed;
        

    }

    public void UnderAttack(Ant _enemy)
    {
        Target = _enemy.transform;
        Attacking = true;
    }


}
