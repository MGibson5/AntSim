using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugColonyKillAnts : MonoBehaviour
{
    public Colony colony;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Ant ant in colony.Ants)
        {
            ant.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
