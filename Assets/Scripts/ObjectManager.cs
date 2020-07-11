using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectManager : MonoBehaviour
{
    PlacementIndicator placementIndicator;
    GameObject objectToSpawn = null;

    Camera aRCamera;
    ARRaycastManager aRRaycastManager;
    GameObject objectToTransform = null;

    float initialTouchDistance = 0f;

    bool removing = false;

    public GameObject objectNotSelectedWarningPanel;

    // Start is called before the first frame update
    void Start()
    {
        // get the components
        placementIndicator = FindObjectOfType<PlacementIndicator>();

        aRCamera = FindObjectOfType<Camera>();
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
            if (!removing)
            {
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);

                    // if this is the first frame of the first touch
                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = aRCamera.ScreenPointToRay(touch.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                            if (hit.transform.tag == "ARObject")
                                objectToTransform = hit.transform.gameObject;

                        if (objectToTransform == null)
                            if (objectToSpawn == null)
                                objectNotSelectedWarningPanel.SetActive(true);
                            else
                                // instantiate the objectToSpawn on the
                                // placementIndicator, with the same position and
                                // rotation
                                Instantiate(objectToSpawn,
                                    placementIndicator.transform.position,
                                    placementIndicator.transform.rotation);
                    }

                    if (objectToTransform != null)
                    {
                        // shoot a raycast from the touch position
                        List<ARRaycastHit> hits = new List<ARRaycastHit>();
                        aRRaycastManager.Raycast(touch.position, hits,
                            TrackableType.Planes);

                        // if the raycast hits an AR Plane, then update position and
                        // rotation of the parent of the objectToTransform
                        if (hits.Count > 0)
                        {
                            objectToTransform.transform.parent.position =
                                hits[0].pose.position;
                            objectToTransform.transform.parent.rotation =
                                hits[0].pose.rotation;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                        objectToTransform = null;
                }

                else
                {
                    Touch touch0 = Input.GetTouch(0);
                    Touch touch1 = Input.GetTouch(1);

                    if (touch1.phase == TouchPhase.Began)
                    {
                        Ray ray = aRCamera.ScreenPointToRay((touch0.position +
                            touch1.position) / 2);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                            if (hit.transform.tag == "ARObject")
                            {
                                objectToTransform = hit.transform.gameObject;
                                initialTouchDistance =
                                    Vector3.Distance(touch0.position,
                                    touch1.position);
                            }
                    }

                    if (objectToTransform != null && initialTouchDistance != 0f)
                        objectToTransform.transform.parent.localScale *=
                            Vector3.Distance(touch0.position, touch1.position) /
                            initialTouchDistance;

                    if (touch0.phase == TouchPhase.Ended ||
                        touch1.phase == TouchPhase.Ended)
                    {
                        objectToTransform = null;
                        initialTouchDistance = 0f;
                    }
                }
            }

            else
            {
                Touch touch = Input.GetTouch(0);

                // if this is the first frame of the first touch
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = aRCamera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                        if (hit.transform.tag == "ARObject")
                            objectToTransform = hit.transform.gameObject;

                    if(objectToTransform != null)
                    {
                        Object.Destroy(objectToTransform.transform.parent.gameObject);
                        objectToTransform = null;
                    }
                }
            }
        }
    }

    public void SetObjectToSpawn(GameObject objectToSpawn)
    {
        this.objectToSpawn = objectToSpawn;
    }

    public void ChangeRemoving()
    {
        removing = !removing;
    }
}
