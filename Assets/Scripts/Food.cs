using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject pickupModel;
    [SerializeField]private int food = 5;

    float scale = 1;
    float scaleSubtract;

    // Start is called before the first frame update
    void Start()
    {
        scaleSubtract = scale / food;
    }



    public void PickUp()
    {
        food -= 1;
        ShrinkFood();
        if(food <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void ShrinkFood()
    {
        scale -= scaleSubtract;

        transform.localScale = new Vector3(scale, scale, scale);
    }
}
