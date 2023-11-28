using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI score;
    public Button continueButton;
    private LoadingManager loadingManager => LoadingManager.instance;
    private void Awake()
    {
        continueButton.onClick.AddListener(Continue);
    }
    public void Continue()
    {
        loadingManager.LoadScene(0, loadingManager.callback);
    }
    private void OnEnable()
    {
        Score(GameManager.instance.score);
        HighScore(GameManager.instance.highScore);
    }
    public void HighScore(int value)
    {
        highScore.text = "High Score: " + value;
    }
    public void Score(int value)
    {
        score.text = "Score: " + value;
    }
}
