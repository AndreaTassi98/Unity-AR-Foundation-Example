using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // the texts to visualize on this Button
    public string[] texts;

    // index to keep track of the current visualized text
    int i = 0;

    // function called when this Button is pressed
    public void ChangeText()
    {
        // get the Text component of the child "Text" of this Button
        Text textObject = transform.Find("Text").GetComponent<Text>();

        // if the index isn't at the end of the texts array
        if(i < texts.Length - 1)
            // then increment it to the next text
            ++i;
        else
            // otherwise, set it to zero and restart from the first text
            i = 0;

        // set the text to visualize on this Button to the text of the array
        // that is placed in the position of the index
        textObject.text = texts[i];
    }
}
