using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandeler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeTickSystem.OnTick += delegate (object sender, TimeTickSystem.OnTickEventArgs e)
        {
            //ColonyHunger(30);

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ColonyHungerTicker(int _ticks)
    {
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
    }*/


}
