using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seen_Food : Seen
{

    public override void ThisObjectSeen(GameObject seenBy_)
    {
        if (seenBy_.GetComponent<GrabObject>().getCarryingObject() == false)
        {
            //Debug.Log("Seen FOOD " + gameObject.name);
            if (seenBy_.GetComponent<MarkFood>())
            {
                seenBy_.gameObject.GetComponent<StateScript>().changeState("Seen Food");

                seenBy_.GetComponent<MarkFood>().setFoodTarget(this.gameObject);
            }
        }
    }
}
