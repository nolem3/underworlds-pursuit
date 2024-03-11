using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTeleportAction", menuName = "BossActions/TeleportAction", order = 4)]
public class TeleportAction : BossAction
{
    [SerializeField] private Vector3 randomPositionMin = new Vector3(-1, -1, 0);
    [SerializeField] private Vector3 randomPositionMax = new Vector3(1, 1, 0);

    public override void DoAction(GameObject bossObject)
    {
        bossObject.transform.position = new Vector3(
            Random.Range(randomPositionMin.x, randomPositionMax.x),
            Random.Range(randomPositionMin.y, randomPositionMax.y),
            Random.Range(randomPositionMin.z, randomPositionMax.z));
    }

    public override void StopAction(GameObject bossObject)
    {
        
    }
}
