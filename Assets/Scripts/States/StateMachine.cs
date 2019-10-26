using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = _availableStates.Values.First(); //Sets defualt State (wander)
        }
        var nextState = CurrentState?.Tick(); //makes sure current state isnt null, it returns a state type, if we return null we stay in current state

        if(nextState != null && nextState != CurrentState?.GetType()) 
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = _availableStates[nextState]; //set current state to new state
        OnStateChanged?.Invoke(CurrentState); //listens for state changes and can react if needbe
    }
}
