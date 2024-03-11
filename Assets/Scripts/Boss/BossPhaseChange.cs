using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPhaseChange : ScriptableObject
{
    [SerializeField] private BossPhase nextPhase; 

    public abstract bool ChangeConditionMet(); 

    public BossPhase GetNextPhase()
    {
        return nextPhase; 
    }
}
