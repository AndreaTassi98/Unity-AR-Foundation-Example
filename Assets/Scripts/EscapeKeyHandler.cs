using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeKeyHandler : MonoBehaviour
{
    public GameObject[] panels;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            int i = 0;
            bool aPanelIsActive = false;
            while(i < panels.Length && !aPanelIsActive)
            {
                if(panels[i].activeInHierarchy)
                    aPanelIsActive = true;
                else
                    ++i;
            }

            if(aPanelIsActive)
                panels[i].GetComponent<PanelHandler>().SetPanelActive(false);
            else
                panels[0].GetComponent<PanelHandler>().SetPanelActive(true);
        }
    }
}
