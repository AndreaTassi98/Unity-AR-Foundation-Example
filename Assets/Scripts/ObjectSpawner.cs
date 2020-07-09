using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSpawner : MonoBehaviour
{
    PlacementIndicator placementIndicator;
    GameObject objectToSpawn = null;

    public GameObject objectNotSelectedWarningPanel;

    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the screen is being touched and this is the first frame that the
        // first touch is being pressed down
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began
            && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
            && GameObject.FindGameObjectsWithTag("Panel").Length == 0)
            if(objectToSpawn == null)
                objectNotSelectedWarningPanel.SetActive(true);
            else
                // instantiate the objectToSpawn on the placementIndicator,
                // with the same position and rotation
                Instantiate(objectToSpawn,
                    placementIndicator.transform.position,
                    placementIndicator.transform.rotation);
    }

    public void SetObjectToSpawn(GameObject objectToSpawn)
    {
        this.objectToSpawn = objectToSpawn;
    }
}
