using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_StartSearchOnStart : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SearchFromColony>().StartSearch();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
