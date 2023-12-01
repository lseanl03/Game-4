using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public GameObject background;
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
        StartCoroutine(ShowBackground());
    }
    public void HighScore(int value)
    {
        highScore.text = "High Score: " + value;
    }
    public void Score(int value)
    {
        score.text = "Score: " + value;
    }
    public IEnumerator ShowBackground()
    {
        yield return new WaitForSeconds(0.5f);
        background.transform.localScale = new Vector3(0, 0, 0);
        background.SetActive(true);
        background.transform.DOScale(new Vector3(1, 1, 1), 1f);
    }
}
