using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickObject : MonoBehaviour
{
    private OnClicked CurrentlySelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // the object identified by hit.transform was clicked
                    // do whatever you want
                    Debug.Log("Clicked on: " + hit.collider.name);
                    if (hit.collider.gameObject.GetComponent<OnClicked>())
                    {
                        //Unhighlite objects
                        CurrentlySelected?.ClickOff();
                        Debug.Log("SELECTED " + CurrentlySelected);
                        CurrentlySelected = hit.collider.gameObject.GetComponent<OnClicked>();
                        CurrentlySelected.Clicked();
                    }
                    else
                    {
                        //Close all UI and Unhighlite

                    }
                }
            }


        }

    }

    public void UnClick()
    {
        CurrentlySelected?.ClickOff();
    }
}
