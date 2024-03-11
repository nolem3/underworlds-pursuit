using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveAction", menuName = "BossActions/MoveAction", order = 3)]
public class MoveAction : BossAction
{
    [SerializeField] private Vector3 randomPositionMin = new Vector3(-1, -1, 0);
    [SerializeField] private Vector3 randomPositionMax = new Vector3(1, 1, 0);
    [SerializeField] private bool moveGivenTime = false;
    [SerializeField] private bool moveGivenSpeed = false;
    [SerializeField] private float moveTime = 1;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float minimumDistanceToMove = 0;
    [SerializeField] private float maximumDistanceToMove = 3;
    private Vector3 moveGoalPos = Vector3.zero;
    int findPosAttempts = 0;

    public override void DoAction(GameObject bossObject)
    {
        FindGoalPos(bossObject);
        FindMoveSpeed(bossObject);
        bossObject.GetComponent<BossController>().SetMoveData(moveGoalPos, moveSpeed);
    }

    public override void StopAction()
    {
        base.StopAction();
    }

    private void FindGoalPos(GameObject bossObject)
    {
        moveGoalPos = bossObject.transform.position;
        findPosAttempts = 0;
        while (findPosAttempts < 100
            && (Vector3.Distance(bossObject.transform.position, moveGoalPos) < minimumDistanceToMove
            || Vector3.Distance(bossObject.transform.position, moveGoalPos) > maximumDistanceToMove))
        {
            moveGoalPos = new Vector3(
            Random.Range(randomPositionMin.x, randomPositionMax.x),
            Random.Range(randomPositionMin.y, randomPositionMax.y),
            Random.Range(randomPositionMin.z, randomPositionMax.z));
            findPosAttempts++;
        }
        if (findPosAttempts > 99)
        {
            Debug.LogWarning("MoveAction unable to find valid move location after " + findPosAttempts + " attempts!");
        }
    }

    private void FindMoveSpeed(GameObject bossObject)
    {
        if (moveGivenSpeed)
        {
            return;
        }
        if (moveGivenTime)
        {
            moveSpeed = Vector3.Distance(moveGoalPos, bossObject.transform.position) / moveTime;
            return;
        }
        Debug.LogError("MoveAction moveGivenSpeed and moveGivenTime are both false!");
        moveSpeed = Vector3.Distance(moveGoalPos, bossObject.transform.position) / (GetEnterTime() + GetExitTime());
    }
}
