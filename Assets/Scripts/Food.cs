using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject pickupModel;
    [SerializeField]private int food = 5;

    float scale = 1;
    float scaleSubtract;

    public List<Colony> ColoniesWithRef { get; set; } //Holds all the colinies that have marked this food


    // Start is called before the first frame update
    void Start()
    {
        ColoniesWithRef = new List<Colony>();

        scaleSubtract = scale / food;
    }



    public void PickUp(Colony _homeColony)
    {
        food -= 1;
        ShrinkFood();

        if (!ColoniesWithRef.Contains(_homeColony)) //Add colony to list of colonies that have marked this
        {
            ColoniesWithRef.Add(_homeColony);
        }

        foreach (Colony colony in ColoniesWithRef)
        {
            if(colony != _homeColony)
            {
                // reduce opinion of _homecolony by -1
                FindObjectOfType<ColonyManager>().ChangeOpinion(colony.ID, _homeColony.ID, -1);
            }
        }

        if (food <= 0)
        {
            foreach (Colony colony in ColoniesWithRef)
            {
                Debug.Log(this.name);
                colony.RemoveMarkedFood(this);
                //colony.MarkedFoods.Remove(this); //remove refrence to this food in all colonies marked foods lists
            }
            Destroy(gameObject);
        }
    }

    private void ShrinkFood()
    {
        scale -= scaleSubtract;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}
