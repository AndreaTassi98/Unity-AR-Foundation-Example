using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour
{
    void OnEnable()
    {
        Button[] buttons =
            gameObject.transform.parent.GetComponentsInChildren<Button>(false);

        foreach(Button button in buttons)
            if(button.transform.parent != gameObject.transform)
                button.interactable = false;
    }

    void OnDisable()
    {
        Button[] buttons =
            gameObject.transform.parent.GetComponentsInChildren<Button>(false);

        foreach(Button button in buttons)
            button.interactable = true;
    }
}
