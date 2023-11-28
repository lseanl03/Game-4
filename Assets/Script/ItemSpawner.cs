using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float countTime;
    [SerializeField] private float spawnTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private Transform startPoint, targetPoint, endPoint;
    [SerializeField] private SlimeFusionEnemy slimeFusionEnemyPrefab;
    [SerializeField] private GameObject itemEnemyHolder;
    protected GameManager gameManager => GameManager.instance;

    private void Start()
    {
        GetSpawnTime();
    }
    public void Update()
    {
        if (!gameManager.startedGame || gameManager.player.isDie || gameManager.endedGame) return;
        if (countTime < spawnTime)
        {
            countTime += Time.deltaTime;
        }
        else
        {
            countTime = 0;
            GetSpawnTime();
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        SlimeFusionEnemy slimeFusion = Instantiate(slimeFusionEnemyPrefab);
        slimeFusion.GetPoint(startPoint, targetPoint, endPoint);
        slimeFusion.ChangeState(SlimeFusionState.MoveTargetPoint);
        slimeFusion.transform.SetParent(itemEnemyHolder.transform);
    }
    float GetSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        return spawnTime;
    }
}
