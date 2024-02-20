using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private int startPhaseIndex = 0;
    private int currentPhaseIndex = 0;
    private BossPhase currentPhase;

    public void SetPhase(int given)
    {
        currentPhaseIndex = given;
        currentPhase = boss.GetPhases()[currentPhaseIndex];
    }

    private void Start()
    {
        SetPhase(startPhaseIndex);
        StartCoroutine("BossLoop");
    }

    private IEnumerator BossLoop()
    {
        currentPhase.DetermineNewAction();
        yield return new WaitForSeconds(currentPhase.GetCurrentActionEnterTime());
        currentPhase.DoCurrentAction(this.gameObject);
        yield return new WaitForSeconds(currentPhase.GetCurrentActionExitTime());
        StartCoroutine("BossLoop");
    }
}
