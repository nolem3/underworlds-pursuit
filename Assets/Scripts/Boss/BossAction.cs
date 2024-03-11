using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "newBossAction", menuName = "BossAction", order = 2)]
public abstract class BossAction : ScriptableObject
{
    [SerializeField] private float enterTime = 0.1f;
    [SerializeField] private float exitTime = 0.5f;

    public abstract void DoAction(GameObject bossObject);

    public virtual float GetEnterTime()
    {
        return enterTime;
    }

    public virtual float GetExitTime()
    {
        return exitTime;
    }

    public abstract void StopAction(GameObject bossObject);
}
