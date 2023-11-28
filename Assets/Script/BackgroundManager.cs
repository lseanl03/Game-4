using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public float speed;
    public float startPosX;
    public float endPosX;
    public List<BackgroundController> backgroundControllerList;
    public static BackgroundManager instance;
    protected GameManager gameManager => GameManager.instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SetPos();
    }
    private void Update()
    {

        SetSpeed();
        MoveBackground();
        CheckRepeatBackground();
    }
    void MoveBackground()
    {
        foreach (var backgroundController in backgroundControllerList)
        {
            backgroundController.Move();
        }
    }
    void CheckRepeatBackground()
    {
        if (gameManager.player.isDie) speed = 0;
        foreach (var backgroundController in backgroundControllerList)
        {
            backgroundController.RepeatBG();
        }
    }
    void SetSpeed()
    {
        foreach (var backgroundController in backgroundControllerList)
        {
            backgroundController.speed = speed;
        }
    }
    void SetPos()
    {
        foreach (var backgroundController in backgroundControllerList)
        {
            backgroundController.startPosX = startPosX;
            backgroundController.endPosX = endPosX;
        }
    }
}
