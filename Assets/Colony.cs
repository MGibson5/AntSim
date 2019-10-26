using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Colony : MonoBehaviour
{
    public int colonySize = 1;
    private Food_Marked[] markedFood;
    private List<Food_Marked> markedFoods = new List<Food_Marked>();
    private int nextColonyGrowthThreshold = 10;
    private int hunger = 5;
    public TextMeshProUGUI foodText;

    private int hungerTick;
    [SerializeField] private int hungerTickMax = 50;

    // Start is called before the first frame update
    void Start()
    {
        nextColonyGrowthThreshold = 10;
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
        
    }

    // Update is called once per frame
    void Update()
    {
        foodText.text = hunger.ToString();
    }

    public int getColonySize()
    {
        return colonySize;
    }

    /*public void changeColonySize(int _changeAmount)
    {
        colonySize += _changeAmount;
        growColony();
    }*/

    public void addMarkedFood(Food_Marked _markedFood)
    {
        //Debug.Log(_markedFood + "markedFood");

        //markedFood[(markedFood.Length)+1] = _markedFood;
        markedFoods.Add(_markedFood);

        //bool alreadyAdded = false;
        //Debug.Log(_markedFood + "markedFood");

        /*for (int i = 0; i < markedFoods.Count; i++)
        {
            if (markedFoods[i].gameObject.name == _markedFood.name)
            {
                alreadyAdded = true;
            }
        }

        if (alreadyAdded != true)
        {
            markedFoods.Add(_markedFood);
        }
        //add _markedFood to markedFood*/
    }

    public void unMarkFood(Food_Marked _food)
    {
        //markedFoods.Find(_food);
        markedFoods.Remove(_food);
        Debug.Log("UNMARKED " + _food.name);
        Destroy(_food.gameObject);
        /*foreach (Food_Marked mFood in markedFoods)
        {
            if(mFood == _food)
            {
                markedFoods.Remove(_food);
            }
        }*/
    }

    public Food_Marked checkForMarkedFood()
    {
        //Debug.Log(markedFood[0].gameObject.name);
        if (markedFoods.Count >= 1)
        {
            return markedFoods[0];
        }
        else { return null;}
    }

    private void growColony()
    {
        if (hunger >= nextColonyGrowthThreshold)
        {
           // Debug.Log("GROW COLONY");
            float x = 1.5f;
            transform.localScale += new Vector3(x, x, x);


            nextColonyGrowthThreshold = nextColonyGrowthThreshold * 2;
            colonySize ++;
            gameObject.GetComponent<ColonySpawnMoreCreatures>().Spawn(colonySize); //just using colony size for now.
        }
        
            
    }

    public void shrinkColony()
    {
        Debug.Log("SHRINK COLONY");
        float x = .5f;
        transform.localScale += new Vector3(x, x, x);

        print(Vector3.Scale(new Vector3(1, 2, 3), new Vector3(2, 3, 4)));

        nextColonyGrowthThreshold = nextColonyGrowthThreshold / 2;
        colonySize--;
        gameObject.GetComponent<ColonySpawnMoreCreatures>().Spawn(colonySize); //just using colony size for now.
    }

    /*public void ColonyHunger(int _ticks)
    {
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
    }*/

    public void AddFood(int _amount)
    {
        hunger += _amount;
        growColony();
    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        hungerTick++;
        //print(hungerTick);
        if (hungerTick >= hungerTickMax)
        {
            hungerTick = 0;
            hunger -= 1;
            if (hunger <= 0)
            {
                if (getColonySize() > 1)
                {
                    shrinkColony();
                    hunger = 10;
                }
                else
                {
                    //COLONY DEAD
                    Debug.Log("COLONY STARVED");
                }
            }
        }
    }
}
