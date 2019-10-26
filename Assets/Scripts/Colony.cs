using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    [SerializeField]private int size = 1;
    public int food { get; private set;}
    [SerializeField] int debugFood;
    [SerializeField] private int nextGrowthThreshold = 2;

    [SerializeField] private Ant ant;
    [SerializeField] private GameObject antParent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(food <= 0)
        {
            Debug.Log("COLONY DEAD");
        } else if(food >= nextGrowthThreshold)
        {
            nextGrowthThreshold *= 2;
            ChangeColonySize(1);
            SpawnNewCreatures(GetColonySize() );
        }
        debugFood = food;
    }

    private void SpawnNewCreatures(int amount)
    {
        for( int i = 0; i < amount; i++ )
        {
            Instantiate(ant, gameObject.transform.position, Quaternion.identity, antParent.transform);
        }
    }
}
