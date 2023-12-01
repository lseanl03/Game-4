using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HackPoint : MonoBehaviour
{
    public int code = 100;
    private AxieManager axieManager => AxieManager.instance;
    public TMP_InputField inputField;
    public Button getCodeButton;
    private void Awake()
    {
        getCodeButton.onClick.AddListener(HackUpgradePoint);
    }
    void HackUpgradePoint()
    {
        if(inputField.text == code.ToString())
        {
            axieManager.UpgradePoint(code);
        }
        inputField.text = string.Empty;
    }
}
