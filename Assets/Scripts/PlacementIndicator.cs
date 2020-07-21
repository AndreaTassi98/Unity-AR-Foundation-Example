using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    // component of the AR Session Origin that manages the AR raycast
    ARRaycastManager aRRaycastManager;

    // placement indicator visual
    GameObject visual;

    // Start is called before the first frame update
    void Start()
    {
        // get the AR Raycast Manager component by finding an object of its type
        // in the scene
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();

        // initialize the placement indicator visual with the actual
        // placement indicator GameObject, that is the first child of the
        // GameObject this script is attached to
        visual = transform.GetChild(0).gameObject;

        // hide the placement indicator visual
        visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // List of ARRaycastHits that will contain the information of the
        // trackable objects hit by the AR raycast
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        // shoot an AR raycast from the center of the screen that detects the
        // trackable planes and save the results in the "hits" List
        aRRaycastManager.Raycast(new Vector2(Screen.width / 2,
            Screen.height / 2), hits, TrackableType.Planes);

        // if the AR raycast hits at least one trackable plane
        if (hits.Count > 0)
        {
            // then update position and rotation of the placement indicator
            // with the ones of the first trackable plane that was hit by the
            // AR raycast
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            // if the placement indicator visual is not active in hierarchy
            if (!visual.activeInHierarchy)
                // then show it
                visual.SetActive(true);
        }
    }
}
