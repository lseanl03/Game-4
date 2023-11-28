using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupDamage : MonoBehaviour
{
    public float duration;
    public TextMeshPro damageValueText;
    private void Awake()
    {
        damageValueText = GetComponent<TextMeshPro>();
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        damageValueText.alpha = 1f;
        Sequence sequence = DOTween.Sequence();
        transform.DOScale(1.2f, duration);
        sequence.Append(transform.DOMoveY(transform.position.y + 1f, duration));
        sequence.Append(damageValueText.DOFade(0, duration));
        sequence.OnComplete(HideObj);
    }
    void HideObj()
    {
        gameObject.SetActive(false);
    }
}
