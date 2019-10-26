using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = -.25f;
    private Vector3 lastPosition;
    public float cameraSize = 37.4f;
    public float zoomSensitivity = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0)
        {
            cameraSize -= (zoomSensitivity * (cameraSize / 70));

            if (cameraSize <= 2.5f)
            {
                cameraSize = 2.5f;
            }
            gameObject.GetComponent<Camera>().orthographicSize = cameraSize;
        }else if(d < 0)
        {
            cameraSize += (zoomSensitivity * (cameraSize / 70));

            
            gameObject.GetComponent<Camera>().orthographicSize = cameraSize;

        }
        
        //Panning
        if (Input.GetMouseButtonDown(2))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            //print(mouseSensitivity * (cameraSize / 10));
            Vector3 delta = Input.mousePosition - lastPosition;
            transform.Translate(delta.x * mouseSensitivity * (cameraSize / 10), delta.y * mouseSensitivity * (cameraSize / 10), 0);
            lastPosition = Input.mousePosition;
        }
    }
}
