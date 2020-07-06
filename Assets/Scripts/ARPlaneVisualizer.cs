using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneVisualizer : MonoBehaviour
{
    ARPlaneManager aRPlaneManager;
    bool visualization = true;

    // Start is called before the first frame update
    void Start()
    {
        // get the components
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(ARPlane plane in aRPlaneManager.trackables)
            plane.gameObject.SetActive(visualization);
    }

    public void ChangePlaneVisualization()
    {
        visualization = !visualization;
    }
}
