using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBoss", menuName = "Boss", order = 0)]
public class Boss : ScriptableObject
{
    [SerializeField] private List<BossPhase> phases = new List<BossPhase>();

    public List<BossPhase> GetPhases()
    {
        return phases;
    }
}
