using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Colony : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        Ants = new List<Ant>();
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
        if (hungerTick >= hungerTickMax)
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
            Ants.Add(_tempAnt);
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
}
