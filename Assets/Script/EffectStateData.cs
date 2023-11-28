using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EffectStateList", menuName = "AxieGame/EffectState")]
public class EffectStateDate : ScriptableObject
{
    public List<EffectStateBase> itemList;
}

[Serializable]
public class EffectStateBase
{
    public float value;
    public float countDown;
    public ItemType itemType;
}
