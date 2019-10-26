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

    private bool growing = false;
    // starting value for the Lerp
    static float t = 0.0f;
    float min = 1;
    float max = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        growing = false;
    }

    // Update is called once per frame
    void Update()
    {
        GrowColony();
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
            growing = true;
            GrowColony();
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

    private void GrowColony()
    {
        if (growing)
        {
            float l = Mathf.Lerp(min, max, t);
            transform.localScale = new Vector3(l, l, l);

            t += .5f * Time.deltaTime;
            if(t > 1.0f)
            {
                min = max;

                max += .5f;
                t = 0;
                growing = false;
            }
        }
    }
}
