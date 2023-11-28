using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public bool isSpawned;
    public int waveIndex;
    public float waveSpawnTime = 1;
    public List <EnemyBase> prefabEnemyList;
    public List<Transform> spawnPosList;
}
public class WaveSpawner : MonoBehaviour
{
    public float countSpawn;
    public float delaySpawnEnemy;
    public GameObject checkPointPrefab;
    public GameObject enemyHolder;
    public GameObject checkPointHolder;
    public List<Wave> waveList;
    public Wave currentWave;

    public List<EnemyBase> enemyList = new List<EnemyBase>();
    protected GameManager gameManager => GameManager.instance;
    protected PoolManager poolManager => PoolManager.instance;

    private void Start()
    {
        currentWave = waveList[0];
    }
    private void Update()
    {
        if (!gameManager.startedGame) return;
        if (CanChangeWave())
        {
            currentWave.isSpawned = false;
            int randomIndex = Random.Range(0, waveList.Count);
            currentWave = waveList[randomIndex];
            enemyList.Clear();
        }
        if (CanSpawnWave())
        {
            countSpawn = 0;
            currentWave.isSpawned = true;
            SpawnWave();
        }
        else
        {
            if (currentWave.isSpawned) return;
            countSpawn += Time.deltaTime;
        }

    }
    bool CanSpawnWave()
    {
        return countSpawn >= currentWave.waveSpawnTime;
    }
    void SpawnWave()
    {
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        foreach (var spawnPoint in currentWave.spawnPosList)
        {
            poolManager.SpawnCheckPoint(spawnPoint.transform);
        }
        yield return new WaitForSeconds(delaySpawnEnemy);
        foreach (var spawnPoint in currentWave.spawnPosList)
        {
            EnemyBase enemyBase = GetEnemy();
            var enemy = Instantiate(enemyBase, spawnPoint.transform.position, enemyBase.transform.rotation);
            enemy.transform.parent = enemyHolder.transform;
            enemyList.Add(enemy);
        }
    }
    EnemyBase GetEnemy()
    {
        return currentWave.prefabEnemyList[Random.Range(0, currentWave.prefabEnemyList.Count)];
    }
    bool CanChangeWave()
    {
        if(enemyList.Count == 0) return false;
        foreach (var enemy in enemyList)
        {
            if (enemy != null) return false;
        }
        return true;
    }
}
