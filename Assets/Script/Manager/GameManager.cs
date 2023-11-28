using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public bool startedGame;
    public bool endedGame;
    public int highScore;
    public int score;
    public float time;
    public Transform playerStartingPos;
    public PlayerController player; 
    public static GameManager instance;
    protected UIManager uiManager => UIManager.instance;
    protected AxieManager axieManager => AxieManager.instance;
    protected GameData gameData => GameData.instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GetUpgradePointData();
        SetPlayerStartingPos();
        StartedGameState(false);
    }
    private void Update()
    {
        UpdateTime();
    }
    void SetPlayerStartingPos()
    {
        player.transform.position = playerStartingPos.position;
    }
    public void StartedGameState(bool state)
    {
        startedGame = state;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }
    public void UpdateScore(int value)
    {
        score += value;
        uiManager.Score(score);
    }
    void GetUpgradePointData()
    {
        highScore = gameData.highScore;
    }
    void UpdateHighScore()
    {
        if (highScore < score)
        {
            highScore = score;
            gameData.highScore = highScore;
        }
    }
    public void UpdateTime()
    {
        if (time > 0 && startedGame)
        {
            time -= Time.deltaTime;
            if(time <= 0) time = 0;
            int timeIndex = Mathf.FloorToInt(time);
            uiManager.Time(timeIndex);
        }
        else if(time <= 0)
        {
            if (!endedGame)
            {
                endedGame = true;
                UpdateHighScore();
                uiManager.EndGamePanelState(true);
                axieManager.UpgradePoint(score);
                Holder.instance.ClearAllChild();
            }
        }
    }
}
