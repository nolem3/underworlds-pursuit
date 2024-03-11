using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealthPhaseChange", menuName = "BossPhaseChanges/HealthPhaseChange", order = 4)]
public class HealthPhaseChange : BossPhaseChange
{
    [SerializeField, Range(0, 100)] private float changeWhenBelowPercent = 50f;

    public override bool ChangeConditionMet()
    {
        return GameObject.FindGameObjectWithTag("Boss").GetComponent<AHealth>().GetHealthRatio() < changeWhenBelowPercent;
    }
}
