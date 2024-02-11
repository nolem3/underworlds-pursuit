using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    [SerializeField] private float seconds = 0.0001f;
    [SerializeField] private float randomOffsetPlusOrMinus = 0;
    void Start()
    {
        float waitTime = seconds + Random.Range(-randomOffsetPlusOrMinus, randomOffsetPlusOrMinus);
        Destroy(gameObject, waitTime);
    }
}
