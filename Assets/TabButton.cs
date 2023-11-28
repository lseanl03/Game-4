using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float initialPosY;
    public bool isSelected;
    private Image background;
    private TabButtonGroup tabButtonGroup;
    void Awake()
    {
        tabButtonGroup = GetComponentInParent<TabButtonGroup>();
        background = GetComponent<Image>();
        SetBackGroundColor(tabButtonGroup.defaultColor);
        initialPosY = transform.localPosition.y;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected) SetBackGroundColor(tabButtonGroup.colorSelected);
        else SetBackGroundColor(tabButtonGroup.defaultColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected) return;
        SetBackGroundColor(tabButtonGroup.colorSelecting);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabButtonGroup.ResetTabButton();
        tabButtonGroup.OpenTabContent(this);
        isSelected = true;
        SetBackGroundColor(tabButtonGroup.colorSelected);
        transform.DOLocalMoveY(initialPosY + 5, 0.25f);
    }
    public void HandleTabButtonInitial()
    {
        isSelected = true;
        tabButtonGroup.OpenTabContent(this);
        SetBackGroundColor(tabButtonGroup.colorSelected);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y +5);
    }
    public void SetBackGroundColor(Color color)
    {
        background.color = color;
    }
}
