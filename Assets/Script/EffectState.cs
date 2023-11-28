using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectState : MonoBehaviour
{
    public float countDown;
    public Image countDownImage;
    private void Update()
    {
        CountDown();
    }
    void CountDown()
    {
        countDown-= Time.deltaTime;
        countDownImage.fillAmount = countDown/10;
        if(countDownImage.fillAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
