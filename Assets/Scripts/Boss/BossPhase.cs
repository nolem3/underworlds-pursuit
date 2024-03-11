using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPhase", menuName = "BossPhase", order = 1)]
public class BossPhase : ScriptableObject
{
    [SerializeField] private List<BossAction> bossActions = new List<BossAction>();
    private BossAction currentAction;
    [SerializeField] private List<BossPhaseChange> phaseChanges = new List<BossPhaseChange>();

    public BossAction DetermineNewAction()
    {
        currentAction = bossActions[Random.Range(0, bossActions.Count)];
        return currentAction;
    }

    public float GetCurrentActionEnterTime()
    {
        return currentAction.GetEnterTime();
    }

    public float GetCurrentActionExitTime()
    {
        return currentAction.GetExitTime();
    }

    public void DoCurrentAction(GameObject bossObject)
    {
        currentAction.DoAction(bossObject);
    }

    public List<BossPhaseChange> GetBossPhaseChanges()
    {
        return phaseChanges;
    }

    public void StopAction(GameObject bossObject)
    {
        if (currentAction != null) currentAction.StopAction(bossObject);
    }
}
