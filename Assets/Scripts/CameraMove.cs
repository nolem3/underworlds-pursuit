using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] private float shiftXThreshold;
    [SerializeField] private float shiftDeltaX;
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float xMin;
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= shiftXThreshold)
        {
            transform.position = new Vector3(
                Math.Min(transform.position.x + shiftSpeed, shiftXThreshold + shiftDeltaX),
                transform.position.y,
                transform.position.z
            );
        }
        else
        {
            transform.position = new Vector3(
                Math.Max(transform.position.x - shiftSpeed, xMin),
                transform.position.y,
                transform.position.z
            );
        }
    }
}
