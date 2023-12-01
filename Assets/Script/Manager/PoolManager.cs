using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("Player Bullet")]
    [SerializeField] private PlayerBullet playerBulletPrefab;
    [SerializeField] private GameObject playerBulletHolder;
    private List<PlayerBullet> playerBulletList = new List<PlayerBullet>();

    [Header("Enemy Bullet")]
    [SerializeField] private DryadMageBullet enemyBulletPrefab;
    [SerializeField] private GameObject enemyBulletHolder;
    private List<DryadMageBullet> enemyBulletList = new List<DryadMageBullet>();

    [Header("Enemy Special Bullet")]
    [SerializeField] private DryadMageBullet enemySpecialBulletPrefab;
    [SerializeField] private GameObject enemySpecialBulletHolder;
    private List<DryadMageBullet> enemySpecialBulletList = new List<DryadMageBullet>();

    [Header("PopupDamage")]
    [SerializeField] private PopupDamage popupDamagePrefab;
    [SerializeField] private GameObject popupDamageHolder;
    public List<PopupDamage> popupDamageList = new List<PopupDamage>();

    [Header("CheckPoint")]
    [SerializeField] private CheckPoint checkPointPrefab;
    [SerializeField] private GameObject checkPointHolder;
    private List<CheckPoint> checkPointList = new List<CheckPoint>();

    public static PoolManager instance;
    private void Awake()
    {
        instance = this;
    }

    //player bullet
    public void SpawnPlayerBullet(Transform spawnPos, int value)
    {
        PlayerBullet playerBullet = GetPlayerBullet();
        if (playerBullet == null)
        {
            playerBullet = Instantiate(playerBulletPrefab, spawnPos.transform.position, playerBulletPrefab.transform.rotation);
            playerBullet.transform.SetParent(playerBulletHolder.transform);
            playerBulletList.Add(playerBullet);
        }
        else
        {
            playerBullet.transform.position = spawnPos.transform.position;
            playerBullet.gameObject.SetActive(true);
        }
        playerBullet.damage = value;
    }
    public PlayerBullet GetPlayerBullet()
    {
        foreach (var playerBullet in playerBulletList)
        {
            if (!playerBullet.gameObject.activeInHierarchy)
            {
                return playerBullet;
            }
        }
        return null;
    }

    //enemy bullet
    public void SpawnEnemyBullet(Transform spawnPos, int value)
    {
        DryadMageBullet enemyBullet = GetEnemyBullet();
        if (enemyBullet == null)
        {
            enemyBullet = Instantiate(enemyBulletPrefab, spawnPos.transform.position, enemyBulletPrefab.transform.rotation);
            enemyBullet.transform.SetParent(enemyBulletHolder.transform);
            enemyBulletList.Add(enemyBullet);
        }
        else
        {
            enemyBullet.transform.position = spawnPos.transform.position;
            enemyBullet.gameObject.SetActive(true);
        }
        enemyBullet.damage = value;
    }
    public DryadMageBullet GetEnemyBullet()
    {
        foreach (var enemyBullet in enemyBulletList)
        {
            if (!enemyBullet.gameObject.activeInHierarchy)
            {
                return enemyBullet;
            }
        }
        return null;
    }

    public void SpawnEnemySpecialBullet(Transform spawnPos)
    {
        DryadMageBullet enemySpecialBullet = GetEnemySpecialBullet();
        if (enemySpecialBullet == null)
        {
            enemySpecialBullet = Instantiate(enemySpecialBulletPrefab, spawnPos.transform.position, enemySpecialBulletPrefab.transform.rotation);
            enemySpecialBullet.transform.SetParent(enemySpecialBulletHolder.transform);
            enemySpecialBulletList.Add(enemySpecialBullet);
        }
        else
        {
            enemySpecialBullet.transform.position = spawnPos.transform.position;
            enemySpecialBullet.gameObject.SetActive(true);
        }
        enemySpecialBullet.transform.DOScale(1f, 2f);
    }
    public DryadMageBullet GetEnemySpecialBullet()
    {
        foreach (var enemySpecialBullet in enemySpecialBulletList)
        {
            if (!enemySpecialBullet.gameObject.activeInHierarchy)
            {
                return enemySpecialBullet;
            }
        }
        return null;
    }
    //popupDamage
    public void SpawnPopupDamage(Transform spawnPos, int value)
    {
        float posX = Random.Range(spawnPos.position.x - 0.2f, spawnPos.position.x + 0.2f);
        float posY = spawnPos.position.y;
        Vector2 pos = new Vector2(posX,posY);
        PopupDamage popupDamage = GetPopupDamage();
        if (popupDamage == null)
        {
            popupDamage = Instantiate(popupDamagePrefab, pos, popupDamagePrefab.transform.rotation);
            popupDamage.transform.SetParent(popupDamageHolder.transform);
            popupDamageList.Add(popupDamage);
        }
        else
        {
            foreach(PopupDamage popup in popupDamageList)
            {
                if (!popup.gameObject.activeSelf)
                {
                    popup.transform.position = pos;
                    popup.gameObject.SetActive(true);
                    break;
                }
            }
        }
        popupDamage.damageValueText.text = value.ToString();

    }
    public PopupDamage GetPopupDamage()
    {
        foreach (var popupDamage in popupDamageList)
        {
            if (!popupDamage.gameObject.activeInHierarchy)
            {
                return popupDamage;
            }
        }
        return null;
    }

    //popupDamage
    public void SpawnCheckPoint(Transform spawnPos)
    {
        float posX = spawnPos.position.x;
        float posY = spawnPos.position.y + 0.5f;
        Vector2 pos = new Vector2(posX, posY);
        CheckPoint checkPoint = GetCheckPoint();
        if (checkPoint == null)
        {
            checkPoint = Instantiate(checkPointPrefab, pos, checkPointPrefab.transform.rotation);
            checkPoint.transform.SetParent(checkPointHolder.transform);
            checkPointList.Add(checkPoint);
        }
        else
        {
            checkPoint.transform.position = pos;
            checkPoint.gameObject.SetActive(true);
        }
    }
    public CheckPoint GetCheckPoint()
    {
        foreach (var checkPoint in checkPointList)
        {
            if (!checkPoint.gameObject.activeInHierarchy)
            {
                return checkPoint;
            }
        }
        return null;
    }
}
