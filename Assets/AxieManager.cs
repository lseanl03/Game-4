using DG.Tweening.Core.Easing;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AxieManager : MonoBehaviour
{
    public int upgradePoints;
    public int axieSelectingIndex = 0;
    public GameObject axieListObj;
    public GameObject selectAxieUI;
    public Button axieChangeButton;
    public Button closeSelectAxieButton;
    public Button startGameButton;
    public TextMeshProUGUI describeText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI upgradePointsText;
    public List<SelectAxie> axieList;
    public AxieData axieData;

    public StatsContent statsContent;
    public SelectAxie currentAxie;

    public static AxieManager instance;
    protected GameManager gameManager => GameManager.instance;
    protected GameData gameData => GameData.instance;
    protected AxieListData axieListData => AxieListData.instance;

    private void Awake()
    {
        instance = this;
        axieChangeButton.onClick.AddListener(ChangeAxie);
        closeSelectAxieButton.onClick.AddListener(HandleCloseSelectAxie);
        startGameButton.onClick.AddListener(HandleStartGame);
    }
    private void Start()
    {
        GetAxie();
        GetUpgradePointData();
        GetCurrentAxie();
        SetPlayer(axieSelectingIndex);
        HandleCloseSelectAxie();
    }
    public void GetCurrentAxie()
    {
        if(gameData.currentAxieName == AxieName.None)
        {
            currentAxie = axieList[0];
        }
        else
        {
            foreach(SelectAxie axie in axieList)
            {
                if (axie.axieName == gameData.currentAxieName)
                {
                    currentAxie = axie;
                }
            }
        }
        axieSelectingIndex = CurrentIndex();
        gameData.currentAxieName = currentAxie.axieName;
    }
    public SelectAxie SelectingAxie()
    {
        return axieList[axieSelectingIndex];
    }
    public int CurrentIndex()
    {
        return axieList.IndexOf(currentAxie);
    }
    public void GetUpgradePointData()
    {
        upgradePoints = gameData.upgradePoint;
        UpgradePointsText();
    }
    public void UpgradePoint(int value)
    {
        upgradePoints += value;
        gameData.upgradePoint = upgradePoints;
        UpgradePointsText();
    }
    public void UpgradePointsText()
    {
        upgradePointsText.text = "Upgrade Points: " + upgradePoints.ToString();
    }
    void ChangeAxie()
    {
        axieSelectingIndex++;
        if(axieSelectingIndex >= axieList.Count) axieSelectingIndex = 0;

        SetAxieActive(axieSelectingIndex);
    }
    void GetAxie()
    {
        for (int i = 0; i < axieList.Count; i++)
        {
            foreach (var axieData in axieData.AxieList)
            {
                if (axieList[i].axieName == axieData.axieName)
                {
                    axieList[i].axieDescribe = axieData.AxieDescribe;
                    axieList[i].speed = axieData.speed;
                    axieList[i].rateFire = axieData.rateFire;
                    axieList[i].health = axieData.health;
                    axieList[i].attackDamage = axieData.attackDamage;

                    axieList[i].idle = axieData.idle;
                    axieList[i].run = axieData.run;
                    axieList[i].move = axieData.move;
                    axieList[i].attack = axieData.attack;
                    axieList[i].takeDamage = axieData.takeDamage;
                    axieList[i].die = axieData.die;
                    axieList[i].takeEffect = axieData.takeEffect;
                    axieList[i].skeletonDataAsset = axieData.skeletonDataAsset;
                }
            }
            GetAxieData(axieList[i], axieListData.axieList[i]);
        }
    }
    void GetAxieData(SelectAxie selectAxie, Axie data)
    {
        if (!axieListData.FileAlreadyExists(selectAxie))
        {
            Axie axie = new Axie();
            data.axieName = selectAxie.axieName;
            data.speed = selectAxie.speed;
            data.rateFire = Mathf.Abs(selectAxie.rateFire);
            data.health = selectAxie.health;
            data.attackDamage = selectAxie.attackDamage;
            //axieListData.axieList.Add(axie);
        }
        else
        {
            selectAxie.speed = data.speed;
            selectAxie.health = data.health;
            selectAxie.attackDamage = data.attackDamage;
            selectAxie.rateFire = data.rateFire;
        }
    }
    void SetAxieActive(int index)
    {
        for (int i = 0; i < axieList.Count; i++)
        {
            if (index == i)
            {
                //currentAxie = axieList[i];
                axieList[i].gameObject.SetActive(true);
                SetStats(axieList[i]);
                SetDes(axieList[i]);
                SetName(axieList[i]);
            }
            else
            {
                axieList[i].gameObject.SetActive(false);
            }
        }
    }
    public void UpdateCurrentAxieData(SelectAxie selectAxie)
    {
        foreach(Axie axie in axieListData.axieList)
        {
            if(axie.axieName == selectAxie.axieName)
            {
                axie.health = selectAxie.health;
                axie.speed = selectAxie.speed;
                axie.attackDamage = selectAxie.attackDamage;
                axie.rateFire = selectAxie.rateFire;
            }
        }
    }
    void ResetAxieList()
    {
        foreach(var axit in axieList)
        {
            axit.gameObject.SetActive(false);
        }
    }
    void SetStats(SelectAxie axie)
    {
        AxieData data = axieData;
        statsContent.GetSpeed(axie.speed, data.minSpeed, data.maxSpeed, data.speedUpgradeValue);
        statsContent.GetRateFire(axie.rateFire, data.minRateFire, data.maxRateFire, data.rateFireUpgradeValue);
        statsContent.GetHealth(axie.health, data.minHealth, data.maxHealth, data.healthUpgradeValue);
        statsContent.GetAttack(axie.attackDamage, data.minAttack, data.maxAttack, data.attackUpgradeValue);
    }
    void SetDes(SelectAxie axie)
    {
        describeText.text = axie.axieDescribe;
    }
    void SetName(SelectAxie axie)
    {
        nameText.text = axie.axieName.ToString();
    }
    void AxieListState(bool state)
    {
        axieListObj.SetActive(state);
    }
    void SelectAxieUIState(bool state)
    {
        selectAxieUI.SetActive(state);
    }
    void HandleCloseSelectAxie()
    {
        AxieListState(false);
        SelectAxieUIState(false);
        if(currentAxie  != null)
        {
            axieSelectingIndex = CurrentIndex();
            SetAxieActive(axieSelectingIndex);
        }
    }
    public void HandleOpenSelectAxie()
    {
        AxieListState(true);
        SelectAxieUIState(true);
        if(currentAxie != null)
        {
            SetAxieActive(axieSelectingIndex);
            SetUpgradeCost();
            UpgradePointsText();
        }
    }
    void HandleStartGame()
    {
        AxieListState(false);
        SelectAxieUIState(false);

        SetPlayer(axieSelectingIndex);

        gameData.currentAxieName = currentAxie.axieName;
    }
    void SetUpgradeCost()
    {
        AxieData data = axieData;
        statsContent.GetSpeedUpgradeCost(data.speedCost);
        statsContent.GetRateFireUpgradeCost(data.rateFireCost);
        statsContent.GetHealthUpgradeCost(data.healthCost);
        statsContent.GetAttackUpgradeCost(data.attackCost);
    }
    public void SetPlayer(int index)
    {
        currentAxie = axieList[index];
        PlayerController player = gameManager.player;
        if (player != null)
        {
            player.GetMoveSpeed(currentAxie.speed);
            player.playerAttack.GetAttackTime(Mathf.Abs(currentAxie.rateFire));
            player.playerAttack.GetDamage(currentAxie.attackDamage);
            player.playerHealth.GetHealth(currentAxie.health);

            player.idle = currentAxie.idle;
            player.run = currentAxie.run;
            player.move = currentAxie.move;
            player.attack = currentAxie.attack;
            player.takeDamage = currentAxie.takeDamage;
            player.die = currentAxie.die;
            player.takeEffect = currentAxie.takeEffect;

            player.GetSkeletonDataAsset(currentAxie.skeletonDataAsset);
        }
    }
}
