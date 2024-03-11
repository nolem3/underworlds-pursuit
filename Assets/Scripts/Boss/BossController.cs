using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private int startPhaseIndex = 0;
    private int currentPhaseIndex = 0;
    private BossPhase currentPhase;
    private Vector3 moveGoalPos;
    private float moveSpeed;

    private void Start()
    {
        SetPhase(startPhaseIndex);
        StartCoroutine("BossLoop");
    }

    /*private void Update()
    {
        
    }*/

    private void FixedUpdate()
    {
        if (transform.position != moveGoalPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveGoalPos, moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetPhase(int given)
    {
        if (given < 0 || given > boss.GetPhases().Count)
        {
            Debug.LogError("SetPhase given out of range index: " + given + ", length of phases is " + boss.GetPhases().Count);
            return;
        }
        currentPhaseIndex = given;
        currentPhase = boss.GetPhases()[currentPhaseIndex];
    }

    public void SetPhase(BossPhase phase)
    {
        if (!boss.GetPhases().Contains(phase))
        {
            Debug.LogError("SetPhase given phase not in boss phases: " + phase);
            return;
        }
        currentPhaseIndex = boss.GetPhases().IndexOf(phase);
        currentPhase = phase;
    }

    private IEnumerator BossLoop()
    {
        currentPhase.DetermineNewAction();
        yield return new WaitForSeconds(currentPhase.GetCurrentActionEnterTime());
        PhaseChangeCheck();
        currentPhase.DoCurrentAction(this.gameObject);
        yield return new WaitForSeconds(currentPhase.GetCurrentActionExitTime());
        PhaseChangeCheck();
        StartCoroutine("BossLoop");
    }

    private void PhaseChangeCheck()
    {
        if (currentPhase == null) return;

        foreach (BossPhaseChange change in currentPhase.GetBossPhaseChanges())
        {
            if (change.ChangeConditionMet())
            {
                StopCoroutine("BossLoop");
                currentPhase.StopAction();
                SetPhase(change.GetNextPhase());
                StartCoroutine("BossLoop");
            }
        }
    }

    public void SetMoveGoalPos(Vector3 goalPos)
    {
        moveGoalPos = goalPos;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetMoveData(Vector3 goalPos, float speed)
    {
        SetMoveGoalPos(goalPos);
        SetMoveSpeed(speed);
    }
}
