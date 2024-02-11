using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private float speed;
    [SerializeField] private bool moveRelative = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (moveRelative) transform.Translate(speed * transform.up * Time.fixedDeltaTime);
        else transform.Translate(moveDir * speed * Time.fixedDeltaTime);
    }
}
