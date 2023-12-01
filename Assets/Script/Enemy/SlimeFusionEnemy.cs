using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SlimeFusionState
{
    SpawnPoint,
    MoveEndPoint,
    MoveTargetPoint,
}
public class SlimeFusionEnemy : EnemyBase
{
    [Header("Slime Fusion Enemy")]
    [Space(10)]
    public SlimeFusionState currentState;
    public float timeToPoint;
    public Transform spawnPoint;
    public AnimationReferenceAsset spawnItem;
    public Item itemPrefab;
    private Transform startPoint, targetPoint, endPoint;
    private BoxCollider2D boxCollider2D;
    private List<Tweener> tweenerList = new List<Tweener>();
    protected override void Awake()
    {
        base.Awake();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected override void Start()
    {
        Run();
    }
    private void Update()
    {
        AutoChangeState();
    }
    public void OnDisable()
    {
        StopTweenList();
    }

    public void ChangeState(SlimeFusionState state)
    {
        if (gameManager.player.isDie) return;
        currentState = state;
        switch (currentState)
        {
            case SlimeFusionState.SpawnPoint:
                HandleSpawnPoint();
                break;
            case SlimeFusionState.MoveEndPoint:
                HandleMoveEndPoint();
                break;
            case SlimeFusionState.MoveTargetPoint:
                HandleMoveTargetPoint();
                break;
        }
    }
    public void GetPoint(Transform start, Transform target, Transform end)
    {
        startPoint = start;
        targetPoint = target;
        endPoint = end;
    }
    protected override void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete)
        {
            Run();
        }
    }
    public void SetStartPoint()
    {
        transform.position = startPoint.position;
    }
    void HandleMoveEndPoint()
    {
        MoveEndPoint();
    }
    void HandleSpawnPoint()
    {
        StartCoroutine(SpawnItem());
    }
    void HandleMoveTargetPoint()
    {
        SetStartPoint();
        MoveTargetPoint();
    }
    public IEnumerator SpawnItem()
    {

        yield return new WaitForSeconds(0.5f);
        if (gameObject == null) yield return null;
        skeletonAnimation.state.SetAnimation(0, spawnItem, false);
        float duration = spawnItem.Animation.Duration;
        yield return new WaitForSeconds(duration/1.25f);
        if (gameObject != null)
        {
            var item = Instantiate(itemPrefab, spawnPoint);
            MoveItemToPos(item);
            yield return new WaitForSeconds(1);
            ChangeState(SlimeFusionState.MoveEndPoint);
        }

    }
    public void MoveItemToPos(Item item)
    {
        Tweener moveY = item.transform.DOMoveY(Random.Range(transform.position.y - 2, transform.position.y - 7), 1f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            item.canGetItem = true;
        });
        tweenerList.Add(moveY);
    }
    public void MoveTargetPoint()
    {
        Tweener moveTarget = transform.DOMove(TargetPointRandom(), timeToPoint).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            ChangeState(SlimeFusionState.SpawnPoint);
        });
        tweenerList.Add(moveTarget);
    }
    public void MoveEndPoint()
    {
        Tweener moveEndPoint = transform.DOMove(endPoint.position, timeToPoint).SetEase(Ease.InExpo).OnComplete(() =>
        {
            Destroy(gameObject);
        });
        tweenerList.Add(moveEndPoint);
    }
    public Vector2 TargetPointRandom()
    {
        Vector2 point = new Vector2(Random.Range(targetPoint.position.x - 5, targetPoint.position.x), targetPoint.position.y);
        return point;
    }
    private void StopTweenList()
    {
        foreach (var tweener in tweenerList)
        {
            if (tweener != null)
            {
                tweener.Kill();
            }
        }
        tweenerList.Clear();
    }

}
