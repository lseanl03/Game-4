using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SelectAxie : MonoBehaviour
{
    public AxieName axieName;
    [TextArea] public string axieDescribe;
    public float speed;
    public float rateFire;
    public int health;
    public int attackDamage;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset run;
    public AnimationReferenceAsset move;
    public AnimationReferenceAsset attack;
    public AnimationReferenceAsset takeDamage;
    public AnimationReferenceAsset die;
    public AnimationReferenceAsset takeEffect;
    public SkeletonDataAsset skeletonDataAsset;
    private SkeletonAnimation skeletonAnimation;
    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    private void Start()
    {
        SetSkeletonData();
        Run();
    }
    private void Update()
    {
        AutoChangeState();
    }
    void AutoChangeState()
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).IsComplete) Run();

    }
    public void SetSkeletonData()
    {
        skeletonAnimation.skeletonDataAsset = skeletonDataAsset;
        skeletonAnimation.Initialize(true);
    }
    public void Run()
    {
        skeletonAnimation.timeScale = 2;
        skeletonAnimation.state.SetAnimation(0, run, true);
    }
    public void TakeEffect()
    {
        skeletonAnimation.state.SetAnimation(0, takeEffect, false);
    }
}
