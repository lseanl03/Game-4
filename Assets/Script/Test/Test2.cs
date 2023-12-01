using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test2 : MonoBehaviour
{
    public int getHealthValue = 0;
    private Test test => Test.instance;
    private void Start()
    {
        if(test != null)
        {
            getHealthValue = test.health;
        }
    }
}
