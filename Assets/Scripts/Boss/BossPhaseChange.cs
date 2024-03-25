using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPhaseChange : ScriptableObject
{
    [SerializeField] private BossPhase nextPhase; 
    [SerializeField] private bool temp = false;

    public abstract bool ChangeConditionMet(); 

    public BossPhase GetNextPhase()
    {
        return nextPhase; 
    }

    public void PhaseChangeEffect()
    {
        if (temp)
        {
            GameObject.FindFirstObjectByType<BossController>().gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        }
    }
}
