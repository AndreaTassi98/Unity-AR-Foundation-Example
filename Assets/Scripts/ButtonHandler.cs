using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public string[] texts;
    int i = 0;

    public void ChangeText()
    {
        Text textObject = transform.Find("Text").GetComponent<Text>();

        if(i < texts.Length - 1)
            ++i;
        else
            i = 0;

        textObject.text = texts[i];
    }
}
