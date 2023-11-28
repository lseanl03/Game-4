using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public ItemSpawner itemSpawner;
    public WaveSpawner waveSpawner;


    public static Spawner instance;

    private void Awake()
    {
        instance = this;
    }

}
