using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 waitTimeRange = new Vector2(1, 1.2f);
    private float waitTime = 0;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private Vector2 spawnPos;

    void Start()
    {
        
    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Count)], spawnPos, Quaternion.identity);
            waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
        }
    }
}
 