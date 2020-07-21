using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // function called when the "Yes Button" of the "Quit Confirmation Panel" is
    // pressed
    public void Quit()
    {
        // quit the application
        Application.Quit();
    }
}
