using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButtonGroup : MonoBehaviour
{
    public List<TabButton> tabButtonList = new List<TabButton>();
    public List<TabContent> tabContentList = new List<TabContent>();

    public Color defaultColor;
    public Color colorSelected;
    public Color colorSelecting;
    private void Start()
    {
        tabButtonList[0].HandleTabButtonInitial();
    }
    public void ResetTabButton()
    {
        foreach (var tabButton in tabButtonList)
        {
            if (tabButton.isSelected && tabButton!= null)
            {
                tabButton.isSelected = false;
                tabButton.SetBackGroundColor(defaultColor);
                tabButton.transform.DOLocalMoveY(tabButton.initialPosY, 0.25f);
            }
        }
    }
    public void OpenTabContent(TabButton tabButton)
    {
        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < tabContentList.Count; i++)
        {
            if (i == index) tabContentList[i].gameObject.SetActive(true);
            else tabContentList[i].gameObject.SetActive(false);
        }
    }
}
