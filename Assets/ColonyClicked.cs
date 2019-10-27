using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyClicked : OnClicked
{
    public GameObject ColonyHighlite;
    public override void Clicked()
    {
        ColonyHighlite.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
