using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private int startPhaseIndex = 0;
    private int currentPhaseIndex = 0;
    private BossPhase currentPhase;
    private BossAction currentAction;
    private Vector3 moveGoalPos;
    private float moveSpeed;

    private void Start()
    {
        SetPhase(startPhaseIndex);
        StartCoroutine("BossLoop");
    }

    private void Update()
    {
        PhaseChangeCheck();
    }

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
        Debug.Log("BossLoop start");
        currentAction = currentPhase.DetermineNewAction();
        if (currentAction is SequenceAction)
        {
            ((SequenceAction)currentAction).Start();
            StartCoroutine("SequenceActionLoop"); 
            yield break;
        }
        yield return new WaitForSeconds(currentPhase.GetCurrentActionEnterTime());
        currentPhase.DoCurrentAction(this.gameObject);
        yield return new WaitForSeconds(currentPhase.GetCurrentActionExitTime());
        Debug.Log("BossLoop end");
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
                StopCoroutine("SequenceActionLoop");
                currentPhase.StopAction(this.gameObject);
                SetPhase(change.GetNextPhase());
                StartCoroutine("BossLoop");
            }
        }
    }

    private IEnumerator SequenceActionLoop()
    {
        Debug.Log("SequenceActionLoop start");
        yield return new WaitForSeconds(currentPhase.GetCurrentActionEnterTime());
        currentPhase.DoCurrentAction(this.gameObject);
        yield return new WaitForSeconds(currentPhase.GetCurrentActionExitTime());
        if (((SequenceAction)currentAction).Finished())
        {
            StartCoroutine("BossLoop");
            Debug.Log("SequenceActionLoop start BossLoop");
        }
        else
        {
            // ((SequenceAction)currentAction).Start();
            StartCoroutine("SequenceActionLoop");
            Debug.Log("SequenceActionLoop start SequenceActionLoop");
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
