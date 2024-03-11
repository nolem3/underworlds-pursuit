using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSequenceAction", menuName = "BossActions/SequenceAction", order = 1)]
public class SequenceAction : BossAction
{
    [SerializeField] private List<BossAction> actions = new List<BossAction>();
    private int currentIndex = -1; 

    public override void DoAction(GameObject bossObject)
    {
        Debug.Log("SequenceAction DoAction, currentIndex " + currentIndex + " changing to " + (currentIndex+1));
        currentIndex++;
        // Debug.Log("currentIndex " + currentIndex + " out of " + actions.Count);
        actions[currentIndex].DoAction(bossObject);
    }
    
    public override void StopAction(GameObject bossObject)
    {
        actions[currentIndex].StopAction(bossObject);
        currentIndex = -1;
    }

    public override float GetEnterTime()
    {
        if (currentIndex < 0) return base.GetEnterTime();
        return actions[currentIndex].GetEnterTime();
    }

    public override float GetExitTime()
    {
        if (currentIndex < 0) return base.GetExitTime();
        return actions[currentIndex].GetExitTime();
    }

    public bool Finished()
    {
        Debug.Log("SequenceAction Finished = " + (currentIndex == actions.Count - 1));
        return currentIndex == actions.Count - 1;
    }

    public void Start()
    {
        currentIndex = -1;
    }
}
