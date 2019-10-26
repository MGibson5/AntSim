using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seen : MonoBehaviour
{
    public virtual void ThisObjectSeen(GameObject seenBy_)
    {
        Debug.Log("Seen Object " + gameObject.name);
    }
}
