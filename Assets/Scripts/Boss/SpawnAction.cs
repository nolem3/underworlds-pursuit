using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSpawnAction", menuName = "BossActions/SpawnAction", order = 2)]
public class SpawnAction : BossAction
{
    [SerializeField] private GameObject prefab;


    public override void DoAction(GameObject bossObject)
    {
        GameObject.Instantiate(prefab, bossObject.transform.position, prefab.transform.rotation);
    }
}
