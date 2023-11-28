using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public static Holder instance;
    public Transform[] holderList;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(GameManager.instance.endedGame)
        ClearAllChild();
    }
    public void ClearAllChild()
    {
        foreach (var holder in holderList)
        {
            foreach(Transform child in holder)
            {
                if(child != null)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}
