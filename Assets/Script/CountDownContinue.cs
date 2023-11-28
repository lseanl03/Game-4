using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownContinue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void ChangeText(string value)
    {
        text.text = value;
    }
}
