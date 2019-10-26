using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStats : MonoBehaviour
{

    [SerializeField]private int size;

    float scale = 1;
    float scaleSubtract;

    // Start is called before the first frame update
    void Start()
    {
        scaleSubtract = scale / size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSize(int _changeBy)
    {
        size = size + _changeBy;
        //Debug.Log("Change food size by: " + _changeBy);
        //Debug.Log("FoodSize : " + size);
        checkFoodSize();
    }

    public void checkFoodSize()
    {
        growFood();
        if(size <= 0 )
        {
            gameObject.GetComponent<Food_Marked>().onDestroy();
           // Destroy(gameObject);
        }
    }

    private void growFood()
    {
        scale -= scaleSubtract;
        
        transform.localScale = new Vector3(scale, scale, scale);        
    }
}
