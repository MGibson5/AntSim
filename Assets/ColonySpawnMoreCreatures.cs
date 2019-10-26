using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonySpawnMoreCreatures : MonoBehaviour
{
    public CreatureStats creature;
    public GameObject[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            int j = Random.Range(0, spawnPoints.Length);
            CreatureStats newCreature = Instantiate(creature, spawnPoints[j].transform.position, Quaternion.identity) ;
            newCreature.GetComponent<CreatureStats>().SetHomeColony(this.gameObject.GetComponent<Colony>());

            newCreature.GetComponent<SearchFromColony>().StartSearch();
        }
    }
}
