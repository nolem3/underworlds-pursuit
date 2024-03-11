using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealthPhaseChange", menuName = "BossPhaseChanges/HealthPhaseChange", order = 4)]
public class HealthPhaseChange : BossPhaseChange
{
    [SerializeField, Range(0, 1)] private float changeWhenBelowPercent = 0.5f;

    public override bool ChangeConditionMet()
    {
        return GameObject.FindGameObjectWithTag("Boss").GetComponent<AHealth>().GetHealthRatio() < changeWhenBelowPercent;
    }
}
