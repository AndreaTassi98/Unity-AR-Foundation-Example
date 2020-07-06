using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour
{
    public GameObject panel;

    public void SetPanelActive(bool active)
    {
        Button[] buttons =
            panel.transform.parent.GetComponentsInChildren<Button>(false);

        panel.SetActive(active);

        foreach(Button button in buttons)
            button.interactable = !active;
    }
}
