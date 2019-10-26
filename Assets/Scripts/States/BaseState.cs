using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState //: MonoBehaviour //Possibly remove monobehaviour
{
    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    protected GameObject gameObject; //allows states to acces gameObject(ant)
    protected Transform transform; //allows states to acces transform(dunno if this is ant or target)

    public abstract Type Tick();

}
