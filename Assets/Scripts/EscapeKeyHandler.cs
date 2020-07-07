using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeKeyHandler : MonoBehaviour
{
    public GameObject quitConfirmationPanel;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");

            if(panels.Length == 0)
                quitConfirmationPanel.SetActive(true);
            else
                panels[0].SetActive(false);
        }
    }
}
