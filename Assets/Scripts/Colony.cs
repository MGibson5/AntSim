using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Colony : MonoBehaviour
{
    public string Name;
    public int ID;
    public Color AntColour;
    public string ColonyName { get; private set; }
    [SerializeField]private int size = 1;
    public int food { get; private set;}
    [SerializeField] int debugFood;
    [SerializeField] private int nextGrowthThreshold = 10;
    [SerializeField] private int nextShrinkThreshold = 0;


    [SerializeField] private Ant ant;
    [SerializeField] private GameObject antParent;

    private bool growing = false;
    // starting value for the Lerp
    static float t = 0.0f;
    float min = 1;
    float max = 1.5f;

    private int hungerTick;
    [SerializeField] private int hungerTickMax = 50;

    [SerializeField]private TextMeshProUGUI foodUI;
    [SerializeField] private TextMeshProUGUI popUI;

    public List<Ant> Ants { get; set; }
    public List<Food> MarkedFoods;


    // Start is called before the first frame update
    void Start()
    {
        ColonyName = ("Colony");

        AntColour = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Ants = new List<Ant>();
        MarkedFoods = new List<Food>();

        SpawnNewCreatures(1);
        popUI.text = Ants.Count.ToString();

        food = 5;
        foodUI.text = food.ToString();
        growing = false;
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;

    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        hungerTick++;
        //Debug.Log("-1 food every " + (hungerTickMax - ((int)(GameSettings.AntSpeed) + (int)(GameSettings.SightDist) + (int)(GameSettings.AntSpawnRate)) * .2) + " seconds");
        //Debug.Log("-" + (300 / (hungerTickMax - ((int)(GameSettings.AntSpeed) + (int)(GameSettings.SightDist) + (int)(GameSettings.AntSpawnRate))) + " per minute"));

        if (hungerTick >= hungerTickMax - ((int)(GameSettings.AntSpeed) + (int)(GameSettings.SightDist) + (int)(GameSettings.AntSpawnRate))) //Uses gamesettings to decrease hunger tick rate
        {
            hungerTick = 0;
            food -= 1;
            ChangeColonyScale(false);
            if (food <= 0)
            {
                if (GetColonySize() > 1)
                {
                    ChangeColonySize(-1);
                    food = 10;
                }
                else
                {
                    //COLONY DEAD
                    Debug.Log("COLONY STARVED");
                }
            }
            foodUI.text = food.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        debugFood = food;
        //popUI.text = Ants.Count.ToString();
        MarkedFoods.RemoveAll(item => item == null); //MOVE THIS SOMEPLACE ELSE

        ChangeColonyScale(true);
    }

    public int GetColonySize()
    {
        return size;
    }

    private void ChangeColonySize(int amount)
    {
        size += amount;
    }

    public void ChangeColonyFood(int amount)
    {
        food += amount;
        foodUI.text = food.ToString();
        if (food <= 0)
        {
            Debug.Log("COLONY DEAD");
        } else if(food >= nextGrowthThreshold)
        {
            nextShrinkThreshold = nextGrowthThreshold;
            nextGrowthThreshold *= 2;
            ChangeColonySize(1);
            SpawnNewCreatures(GetColonySize() );
            growing = true;
            ChangeColonyScale(true);
        } else if (food < nextShrinkThreshold)
        {
            nextGrowthThreshold = nextShrinkThreshold;
            nextShrinkThreshold /= 2;
            ChangeColonySize(-1);
            growing = true;
            ChangeColonyScale(false);
        }
        debugFood = food;
    }

    private void SpawnNewCreatures(int amount)
    {
        for( int i = 0; i < amount; i++ )
        {
            Ant _tempAnt = Instantiate(ant, gameObject.transform.position, Quaternion.identity, antParent.transform);
            _tempAnt.AntColour = AntColour;
            Ants.Add(_tempAnt);
            _tempAnt.HomeColony = (this);
            _tempAnt.gameObject.SetActive(true);
            popUI.text = Ants.Count.ToString();
        }
    }

    private void ChangeColonyScale(bool _grow)
    {
        if (_grow)
        {
            if (growing)
            {
                float l = Mathf.Lerp(min, max, t);
                transform.localScale = new Vector3(l, l, l);

                t += .5f * Time.deltaTime;
                if (t > 1.0f)
                {
                    min = max;

                    max += .5f;
                    t = 0;
                    growing = false;
                }
            }
        }
        else
        {
            if (growing)
            {
                float l = Mathf.Lerp((max-.5f), min, t);
                transform.localScale = new Vector3(l, l, l);

                t += .5f * Time.deltaTime;
                if (t > 1.0f)
                {
                    min = max;

                    max -= .5f;
                    t = 0;
                    growing = false;
                }
            }
        }
    }

    public void AddMarkedFood(Food _markedFood)
    {
        if (!MarkedFoods.Contains(_markedFood))
        {
            MarkedFoods.Add(_markedFood);
            Debug.Log(_markedFood.gameObject.name + " marked");
        }
    }

    public void RemoveMarkedFood(Food _markedFood)
    {

        if (MarkedFoods.Contains(_markedFood))
        {
            MarkedFoods.Remove(_markedFood);
            MarkedFoods.RemoveAll(item => item == null);

            //Debug.Log(MarkedFoods[MarkedFoods.Count]);
            //MarkedFoods.Sort();
            //Debug.Log(_markedFood.gameObject.name + " unmarked");
            /*if(MarkedFoods.Count == 1)
            {
                Debug.Log("NULL");
                MarkedFoods.Clear();
            }*/
        }
    }
}
