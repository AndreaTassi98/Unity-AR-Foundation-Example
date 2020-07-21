using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    // function called when the "Yes Button" of the "Remove All Panel"
    // is pressed
    public void RemoveAll()
    {
        // array of GameObjects that contains all the placed AR objects that
        // are active in the scene
        GameObject[] aRObjects = GameObject.FindGameObjectsWithTag("ARObject");

        // for each AR object in the array
        foreach(GameObject aRObject in aRObjects)
            // remove it from the scene by destroying its parent GameObject
            Object.Destroy(aRObject.transform.parent.gameObject);
    }
}
