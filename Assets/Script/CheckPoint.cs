using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public float hideTime;
    private Transform initialPos;
    private void Awake()
    {
        initialPos = transform;
    }
    private void OnEnable()
    {
        transform.DOScale(transform.localScale.x + 0.05f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        StartCoroutine(HideObj());
    }
    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = initialPos.localScale;
    }
    IEnumerator HideObj()
    {
        yield return new WaitForSeconds(hideTime);
        gameObject.SetActive(false);
    }
}
