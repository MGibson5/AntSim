using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    public Colony HomeColony;
    [SerializeField]private int hunger;
    private int maxHunger = 10;

    private int hungerTick;
    [SerializeField] private int hungerTickMax = 50;

    // Start is called before the first frame update
    void Start()
    {
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
            if (hunger <= maxHunger/10)
            {
                //return To Colony for food
            }
            if(hunger <= 0)
            {
                if(gameObject != null)
                    Destroy(gameObject);
            }
        }
    }

    public void changeHunger(int _amountChange)
    {
        hunger += _amountChange;
        if (hunger > maxHunger)
            hunger = maxHunger;
    }


    public Colony getHomeColony()
    {
        return HomeColony;
    }

    public void SetHomeColony(Colony _homeColony)
    {
        HomeColony = _homeColony;
    }
}
