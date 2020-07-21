using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectManager : MonoBehaviour
{
    // the placement indicator
    PlacementIndicator placementIndicator;

    // the AR object to spawn on top of the placement indicator
    GameObject objectToSpawn = null;

    // the AR Camera child of the AR Session Origin
    Camera aRCamera;

    // component of the AR Session Origin that manages the AR raycast
    ARRaycastManager aRRaycastManager;

    // the placed AR object to transform
    GameObject objectToTransform = null;

    // the distance between the two touches in the previous frame
    float previousTouchDistance = 0f;

    // the angle formed by the two touches segment and the positive direction
    // of the horizontal axis in the previous frame
    float previousTouchAngle = 0f;

    // object removing mode
    bool removing = false;

    // the panel to show if no object to spawn is selected
    public GameObject objectNotSelectedWarningPanel;

    // Start is called before the first frame update
    void Start()
    {
        // get the placement indicator by finding an object of its type in the
        // scene
        placementIndicator = FindObjectOfType<PlacementIndicator>();

        // get the AR Camera by finding an object of type Camera in the scene
        aRCamera = FindObjectOfType<Camera>();

        // get the AR Raycast Manager component by finding an object of its type
        // in the scene
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the screen is being touched (not on a button) and there isn't any
        // active panel
        if(Input.touchCount > 0 &&
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
            && GameObject.FindGameObjectsWithTag("Panel").Length == 0)
        {
            // if the object removing mode is off
            if (!removing)
            {
                // if the input has only one touch
                if (Input.touchCount == 1)
                {
                    // get the first touch of the input
                    Touch touch = Input.GetTouch(0);

                    // if this is the first frame of the first touch
                    if (touch.phase == TouchPhase.Began)
                    {
                        // instantiate the ray of the physical raycast from the
                        // touch position
                        Ray ray = aRCamera.ScreenPointToRay(touch.position);

                        // output variable that contains the information of the
                        // first object hit by the physical raycast
                        RaycastHit hit;

                        // if the physical raycast hits an object with a
                        // Collider component
                        if (Physics.Raycast(ray, out hit))
                            // if the hit object is a placed AR object
                            if (hit.transform.tag == "ARObject")
                                // then set the object to transform
                                // to its GameObject
                                objectToTransform = hit.transform.gameObject;

                        // if the physical raycast has not hit any placed
                        // AR object, then the user wants to place a new
                        // AR object
                        if (objectToTransform == null)
                            // if the object to spawn has not been selected
                            if (objectToSpawn == null)
                                // then show the warning panel
                                objectNotSelectedWarningPanel.SetActive(true);
                            else
                                // if the object to spawn has been selected,
                                // then instantiate it on the placement
                                // indicator, with the same position and
                                // rotation
                                Instantiate(objectToSpawn,
                                    placementIndicator.transform.position,
                                    placementIndicator.transform.rotation);
                    }

                    // if the physical raycast has hit a placed AR object,
                    // then the user wants to translate it
                    if (objectToTransform != null)
                    {
                        // List of ARRaycastHits that will contain the
                        // information of the trackable objects hit by the
                        // AR raycast
                        List<ARRaycastHit> hits = new List<ARRaycastHit>();

                        // shoot an AR raycast from the touch position that
                        // detects the trackable planes and save the results
                        // in the "hits" List
                        aRRaycastManager.Raycast(touch.position, hits,
                            TrackableType.Planes);

                        // if the AR raycast hits at least one trackable plane
                        if (hits.Count > 0)
                        {
                            // then update position and rotation of the parent
                            // of the object to transform with the ones of the
                            // first trackable plane that was hit by the
                            // AR raycast
                            objectToTransform.transform.parent.position =
                                hits[0].pose.position;
                            objectToTransform.transform.parent.rotation =
                                hits[0].pose.rotation;
                        }
                    }

                    // if this is the last frame of the first touch
                    if (touch.phase == TouchPhase.Ended)
                        // then set the object to transform to null
                        objectToTransform = null;
                }

                else
                {
                    // if the input has more than one touch,
                    // then get the first two touches
                    Touch touch0 = Input.GetTouch(0);
                    Touch touch1 = Input.GetTouch(1);

                    // if this is the first frame of the first touch
                    if (touch1.phase == TouchPhase.Began)
                    {
                        // instantiate the ray of the physical raycast from the
                        // middle point between the two touches positions
                        Ray ray = aRCamera.ScreenPointToRay((touch0.position +
                            touch1.position) / 2);

                        // output variable that contains the information of the
                        // first object hit by the physical raycast
                        RaycastHit hit;

                        // if the physical raycast hits an object with a
                        // Collider component
                        if (Physics.Raycast(ray, out hit))
                            // if the hit object is a placed AR object
                            if (hit.transform.tag == "ARObject")
                            {
                                // set the object to transform to its GameObject
                                objectToTransform = hit.transform.gameObject;

                                // save the initial distance between the two
                                // touches
                                previousTouchDistance =
                                    Vector3.Distance(touch0.position,
                                    touch1.position);

                                // save the initial angle formed by the two
                                // touches segment and the positive direction
                                // of the horizontal axis, with all the vectors
                                // rotating around the positive direction
                                // of the z axis
                                previousTouchAngle =
                                    Vector3.SignedAngle(touch1.position -
                                    touch0.position, transform.right,
                                    transform.forward);
                            }
                    }

                    // if the physical raycast has hit a placed AR object,
                    // then the user wants to scale and/or rotate it
                    if (objectToTransform != null)
                    {
                        // avoid divisions by zero
                        if(previousTouchDistance != 0f)
                        {
                            // save the distance between the two touches in the
                            // current frame
                            float currentTouchDistance =
                            Vector3.Distance(touch0.position, touch1.position);

                            // scale the parent of the object to transform
                            // by a factor that depends on the ratio of
                            // the distances in the current frame and
                            // the previous one
                            objectToTransform.transform.parent.localScale *=
                                currentTouchDistance / previousTouchDistance;

                            // update the distance between the two touches in
                            // the previous frame with the one in the current
                            // frame
                            previousTouchDistance = currentTouchDistance;
                        }

                        // avoid divisions by zero
                        if(previousTouchAngle != 0f)
                        {
                            // save the angle in the current frame
                            float currentTouchAngle =
                            Vector3.SignedAngle(touch1.position -
                                    touch0.position, transform.right,
                                    transform.forward);

                            // rotate the parent of the object to transform
                            // around the axis that is parallel to the normal
                            // of the plane its placed onto,
                            // by a factor that depends on the difference
                            // between the angles in the current frame and
                            // the previous one
                            objectToTransform.transform.parent.Rotate(
                                transform.up,
                                currentTouchAngle - previousTouchAngle);

                            // avoid divisions by zero in the next frames
                            if(currentTouchAngle != 0f)
                                // update the angle in the previous frame with
                                // the one in the current frame
                                previousTouchAngle = currentTouchAngle;
                        }
                    }

                    // if this is the last frame of the first
                    // or the second touch
                    if (touch0.phase == TouchPhase.Ended ||
                        touch1.phase == TouchPhase.Ended)
                    {
                        // set the object to transform to null
                        objectToTransform = null;

                        // set the distance in the previous frame to zero
                        previousTouchDistance = 0f;

                        // set the angle in the previous frame to zero
                        previousTouchAngle = 0f;
                    }
                }
            }

            else
            {
                // if the object removing mode is on,
                // then get the first touch of the input
                Touch touch = Input.GetTouch(0);

                // if this is the first frame of the first touch
                if (touch.phase == TouchPhase.Began)
                {
                    // instantiate the ray of the physical raycast from the
                    // touch position
                    Ray ray = aRCamera.ScreenPointToRay(touch.position);

                    // output variable that contains the information of the
                    // first object hit by the physical raycast
                    RaycastHit hit;

                    // if the physical raycast hits an object with a
                    // Collider component
                    if (Physics.Raycast(ray, out hit))
                        // if the hit object is a placed AR object
                        if (hit.transform.tag == "ARObject")
                            // then set the object to transform
                            // to its GameObject
                            objectToTransform = hit.transform.gameObject;

                    // if the physical raycast has hit a placed AR object,
                    // then the user wants to remove it from the scene
                    if (objectToTransform != null)
                    {
                        // remove the object from the scene by destroying
                        // its parent GameObject
                        Object.Destroy(objectToTransform.transform.parent.gameObject);

                        // set the object to transform to null
                        objectToTransform = null;
                    }
                }
            }
        }
    }

    // function called when an object is selected from the "Select Object Panel"
    // (the objectToSpawn argument is passed by the relative button event)
    public void SetObjectToSpawn(GameObject objectToSpawn)
    {
        // set the object to spawn to the selected object
        this.objectToSpawn = objectToSpawn;
    }

    // function called when the "Remove Object" or the "Stop Removing" buttons
    // are pressed
    public void ChangeRemoving()
    {
        // change the object removing mode
        removing = !removing;
    }
}
