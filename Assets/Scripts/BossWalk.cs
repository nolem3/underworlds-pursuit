using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : MonoBehaviour
{

    [SerializeField] private Vector2 walkVelocity = new Vector2(1, 0);

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(walkVelocity * Time.deltaTime);
        // transform.position = new Vector3(transform.position.x + walkSpeed, transform.position.y, transform.position.z) * Time.deltaTime;
        //Debug.Log(transform.position.x + walkSpeed);
    }
}
