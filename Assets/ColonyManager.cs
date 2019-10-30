using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyManager : MonoBehaviour
{
    public int[,] ColoniesOpinionTable;

    // Start is called before the first frame update
    void Start()
    {
        //Initilise 2d array
        Colony[] allColonies = FindObjectsOfType<Colony>();
        ColoniesOpinionTable = new int[allColonies.Length , allColonies.Length];

        
        for(int i = 0; i < allColonies.Length; i++)
        {
            allColonies[i].ID = i;
            for (int j = 0; j < allColonies.Length; j++)
            {
                ColoniesOpinionTable[i,j] = 50; //Set starting opinion to 50 which is netural
            }
        }
    }

    public void ChangeOpinion(int _1stColonyID, int _2ndColonyID, int _changeBy)
    {
        ColoniesOpinionTable[_1stColonyID, _2ndColonyID] += _changeBy;
    }

    public int GetOpinion(int _1stColonyID, int _2ndColonyID)
    {
        //Gets the opinion colony 1 has of colony 2
        return ColoniesOpinionTable[_1stColonyID, _2ndColonyID];
    }


}
