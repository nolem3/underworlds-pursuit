using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 globalDistanceMovedBetweenSpawnsRange = new Vector2(4, 5.2f);
    private float waitTime = 0;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private bool preventRepeats;
    [SerializeField] private Vector2 spawnPos;
    private Vector2 spawnPosVaried;
    [SerializeField] private Vector2 spawnVariation = new Vector2(0, 1);
    int randomIndex;
    int lastIndex;

    void Start()
    {
        
    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            randomIndex = Random.Range(0, prefabs.Count);
            while (preventRepeats && lastIndex == randomIndex)
            {
                randomIndex = Random.Range(0, prefabs.Count);
            }
            spawnPosVaried = spawnPos;
            spawnPosVaried += new Vector2(Random.Range(-1 * spawnVariation.x, spawnVariation.x), Random.Range(-1 * spawnVariation.y, spawnVariation.y));
            Instantiate(prefabs[randomIndex], spawnPosVaried, Quaternion.identity);
            waitTime = Random.Range(globalDistanceMovedBetweenSpawnsRange.x, globalDistanceMovedBetweenSpawnsRange.y) / GlobalMove.globalSpeed;
            lastIndex = randomIndex;
        }
    }
}
 