using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealthPhaseChange", menuName = "BossPhaseChanges/HealthPhaseChange", order = 4)]
public class HealthPhaseChange : BossPhaseChange
{
    [SerializeField] private AHealth healthScript;
    [SerializeField] private float changeWhenBelowPercent = 0.5f;

    public override bool ChangeConditionMet()
    {
        // return healthScript.GetHealthRatio() < 0.5f;
        return GameObject.FindGameObjectWithTag("Boss").GetComponent<AHealth>().GetHealthRatio() < 0.5f;
    }
}
