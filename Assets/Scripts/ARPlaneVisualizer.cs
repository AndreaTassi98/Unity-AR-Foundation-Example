using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneVisualizer : MonoBehaviour
{
    // component of the AR Session Origin that manages the
    // trackable planes detection
    ARPlaneManager aRPlaneManager;

    // detected planes visualization mode
    bool visualization = true;

    // Start is called before the first frame update
    void Start()
    {
        // get the AR Plane Manager component by finding an object of its type
        // in the scene
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // for each AR Plane in the trackable planes detected
        // by the AR Plane Manager
        foreach(ARPlane plane in aRPlaneManager.trackables)
            // activate or deactivate its visualization,
            // depending on the detected planes visualization mode
            plane.gameObject.SetActive(visualization);
    }

    // function called when the "Hide Planes" or the "Show Planes" buttons
    // are pressed
    public void ChangePlaneVisualization()
    {
        // change the detected planes visualization mode
        visualization = !visualization;
    }
}
