using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    PlacementIndicator placementIndicator;
    GameObject objectToSpawn = null;

    public GameObject placeObjectWarningPanel;

    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    public void SetObjectToSpawn(GameObject objectToSpawn)
    {
        this.objectToSpawn = objectToSpawn;
    }

    public void SpawnObject()
    {
        if(objectToSpawn == null)
            placeObjectWarningPanel.SetActive(true);
        else
            // instantiate the objectToSpawn on the placementIndicator, with
            // the same position and rotation
            Instantiate(objectToSpawn, placementIndicator.transform.position,
                placementIndicator.transform.rotation);
    }
}
