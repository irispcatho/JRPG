using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public static ZoomCamera instance;
    public bool zoomActive;
    //public Vector3[] target;
    public Camera cam;

    public float speed;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
    }

    public void LateUpdate()
    {
        if(zoomActive)
        {
            Vector3 whereTheCamIs = cam.transform.position;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, whereTheCamIs, speed);
        }
        else
        {
            Vector3 whereTheCamIs = cam.transform.position;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, whereTheCamIs, speed);
        }
    }
}
