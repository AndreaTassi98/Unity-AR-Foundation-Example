using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private PlacementIndicator placementIndicator;

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
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // then instantiate the objectToSpawn on the placementIndicator,
            // with the same position and rotation
            GameObject obj = Instantiate(objectToSpawn,
                placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
    }
}
