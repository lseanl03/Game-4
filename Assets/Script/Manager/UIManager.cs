using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI TimeText;
    public Button startButton;
    public Button normalAttack;
    public Button quitButton;
    public GameObject joyStick;
    public GameObject skill;
    public EndGamePanel endGamePanel;
    public EnemyInfoPanel enemyInfoPanel;
    public HealthBar healthBar;
    public EffectBar effectBar;
    public Setting setting;

    public static UIManager instance;
    protected BackgroundManager backgroundManager => BackgroundManager.instance;
    protected GameManager gameManager => GameManager.instance;
    protected AxieManager axieManager => AxieManager.instance;
    private void Awake()
    {
        instance = this;
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }
    private void Start()
    {
        SettingInitial();
    }
    void SkillState(bool state)
    {
        skill.SetActive(state);
    }
    void JoyStickState(bool state)
    {
        joyStick.SetActive(state);
    }
    void StartButtonState(bool state)
    {
        startButton.gameObject.SetActive(state);
    }
    public void EndGamePanelState(bool state)
    {
        endGamePanel.gameObject.SetActive(state);
    }
    public void EnemyInfoPanelState(bool state)
    {
        enemyInfoPanel.gameObject.SetActive(state);
    }

    public void Score(int value)
    {
        scoreText.text = "Score: " + value;
    }
    public void Time(int value)
    {
        TimeText.text = "Time: " + value;
    }
    void StartGame()
    {
        SkillState(true);
        JoyStickState(true);
        StartButtonState(false);
        gameManager.StartedGameState(true);
        TimeText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        setting.pauseGameButton.gameObject.SetActive(true);
        setting.selectAxieButton.gameObject.SetActive(false);
        setting.enemyInfoButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }
    void SettingInitial()
    {
        JoyStickState(true);
        SkillState(false);
        TimeText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
