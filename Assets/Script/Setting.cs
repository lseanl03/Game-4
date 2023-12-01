using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public bool isPaused;
    public Button selectAxieButton;
    public Button enemyInfoButton;
    public Button pauseGameButton;
    public Button resetGameButton;
    private GameManager gameManager=> GameManager.instance;
    private UIManager uiManager => UIManager.instance;
    private AxieManager axieManager => AxieManager.instance;
    private LoadingManager loadingManager => LoadingManager.instance;
    private void Awake()
    {
        selectAxieButton.onClick.AddListener(OpenSelectAxie);
        pauseGameButton.onClick.AddListener(PauseGame);
        resetGameButton.onClick.AddListener(ResetGame);
        enemyInfoButton.onClick.AddListener(OpenEnemyInfo);
    }
    private void Start()
    {
        ResetGameState(false);
    }
    void PauseGame()
    {
        if (gameManager.startedGame)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                ResetGameState(true);
                gameManager.PauseGame();
            }
            else
            {
                ResetGameState(false);
                gameManager.ContinueGame();
            }
        }
    }
    void ResetGame()
    {
        gameManager.ContinueGame();
        loadingManager.LoadScene(0, loadingManager.callback);
    }
    void ResetGameState(bool state)
    {
        resetGameButton.gameObject.SetActive(state);
    }
    void OpenSelectAxie()
    {
        axieManager.HandleOpenSelectAxie();
    }
    void OpenEnemyInfo()
    {
        uiManager.EnemyInfoPanelState(true);
    }
}
