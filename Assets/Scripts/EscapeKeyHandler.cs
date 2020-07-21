using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeKeyHandler : MonoBehaviour
{
    // the panel to show when the "Escape" key is pressed and there isn't any
    // active panel
    public GameObject quitConfirmationPanel;

    // Update is called once per frame
    void Update()
    {
        // if the "Escape" key of the device is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // array of GameObjects that contains all the active panels
            // in the scene
            GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");

            // if there isn't any active panel
            if(panels.Length == 0)
                // then show the "Quit Confirmation Panel"
                quitConfirmationPanel.SetActive(true);
            else
                // if there is at least one active panel,
                // then deactivate the first panel of the array
                panels[0].SetActive(false);
        }
    }
}
