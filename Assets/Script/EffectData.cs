using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemList", menuName = "AxieGame/Item")]
public class EffectData : ScriptableObject
{
    public List<ItemBase> itemList;
}

[Serializable]
public class ItemBase
{
    public float countDown;
    public ItemType itemType;
    public Sprite itemSprite;
}
