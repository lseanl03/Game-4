using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBar : MonoBehaviour
{
    public EffectState buffMoveSpeedEffect;
    public EffectState buffShieldEffect;
    public EffectState buffAttackEffect;
    public void BuffMoveSpeedState(bool state, float countDown)
    {
        ChangeState(buffMoveSpeedEffect, state, countDown);
    }
    public void BuffShieldState(bool state, float countDown)
    {
        ChangeState(buffShieldEffect, state, countDown);
    }
    public void BuffAttackState(bool state, float countDown)
    {
        ChangeState(buffAttackEffect, state, countDown);
    }
    void ChangeState(EffectState effectState, bool state, float countDown)
    {
        if (state)
        {
            effectState.transform.SetAsLastSibling();
            effectState.countDown = countDown;
        }
        effectState.gameObject.SetActive(state);
    }
}
