using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetGamePanel : MonoBehaviour
{
    public Button resetButton;
    private void Start()
    {
        resetButton.onClick.AddListener(ResetGame);
    }
    void ResetGame()
    {
        Debug.Log("a");
        //GameManager.instance.ResetGame();
    }
}
