using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour
{
    // OnEnable is called when the GameObject this script is attached to
    // is activated
    void OnEnable()
    {
        // array of Buttons that contains all the active buttons of the parent
        // Canvas or panel of this panel
        Button[] buttons =
            gameObject.transform.parent.GetComponentsInChildren<Button>(false);

        // for each button of the array
        foreach(Button button in buttons)
            // if the button parent is not this panel
            if(button.transform.parent != gameObject.transform)
                // then make the button not interactable
                button.interactable = false;
    }

    // OnDisable is called when the GameObject this script is attached to
    // is deactivated
    void OnDisable()
    {
        // array of Buttons that contains all the active buttons of the parent
        // Canvas or panel of this panel
        Button[] buttons =
            gameObject.transform.parent.GetComponentsInChildren<Button>(false);

        // for each button of the array
        foreach(Button button in buttons)
            // make the button interactable
            button.interactable = true;
    }
}
