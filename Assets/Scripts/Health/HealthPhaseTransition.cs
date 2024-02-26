using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPhaseTransition : AHealthTracker
{
    [SerializeField] private float minRatioToTransition = 0.5f;
    [SerializeField] private BossController boss;
    [SerializeField] private int phaseID;
    private bool done = false;

    public override void HealthChanged(AHealth healthScript)
    {
        if (done) return;
        if (healthScript.GetHealthRatio() <= minRatioToTransition)
        {
            done = true;
            boss.SetPhase(phaseID);
        }
    }
}
