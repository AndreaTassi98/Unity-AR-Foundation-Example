using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    public void RemoveAll()
    {
        GameObject[] aRObjects = GameObject.FindGameObjectsWithTag("ARObject");

        foreach(GameObject aRObject in aRObjects)
            Object.Destroy(aRObject.transform.parent.gameObject);
    }
}
