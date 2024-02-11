using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMove : MonoBehaviour
{
    public static float globalSpeed = 4;
    [SerializeField] private Vector2 moveDir = new Vector2(-1, 0);

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(moveDir * globalSpeed * Time.fixedDeltaTime); 
    }
}
