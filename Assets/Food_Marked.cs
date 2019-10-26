using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Marked : MonoBehaviour
{
    private List<Colony> markedBy = new List<Colony>();


    [SerializeField]private Colony[] markedBy_;
    private int counter;
    //THIS script could also be used to see what colonies have marked this food using this list/array ^

    private void Start()
    {
        int i = FindObjectsOfType<Colony>().Length;
        //markedBy_ = new Colony[i];
        //counter = 0;
    }

    public void setMarked(Colony homeColony)
    {
       /* bool alreadyAdded = false;
        Debug.Log("setMarked!");

        //check if already in the colony
        for (int i = 0; i < markedBy_.Length; i++)
        {
            if(markedBy_[i] == homeColony)
            {
                Debug.Log("Food Already added");
                alreadyAdded = true;
            } else
            {
                alreadyAdded = false;
            }
        }

        if(alreadyAdded == false)
        {
            markedBy_[counter] = homeColony;
            homeColony.addMarkedFood(this);

            counter++;
        }*/


        //LIST CODE V (can be used instead or array code above)
        if (!markedBy.Contains(homeColony))
        {
            markedBy.Add(homeColony);
            homeColony.addMarkedFood(this);
        }
    }

    public void onDestroy()
    {
        //markedBy.ForEach(Colony colony in );

        foreach(Colony colony in markedBy)
        {
            colony.unMarkFood(this);
        }
        //markedBy_[counter].unMarkFood(this);
    }


}