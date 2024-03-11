using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    void Start()
    {
        rigidBody.velocity = new Vector2(0, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float downSpeed = -1 * moveSpeed;
        if (transform.position.y >= maxY)
        {
            rigidBody.velocity = new Vector2(0, downSpeed);
        }
        else if (transform.position.y <= minY)
        {
            rigidBody.velocity = new Vector2(0, moveSpeed);
        }
    }
}
