using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMove : MonoBehaviour
{
    public static float globalSpeed = 4;
    [SerializeField] private Vector2 moveDir = new Vector2(-1, 0);
    [SerializeField] private Vector2 moveDirRandomRange = new Vector2(0, 0);

    private void Start()
    {
        moveDir += new Vector2(Random.Range(-1 * moveDirRandomRange.x, moveDirRandomRange.x),
                                Random.Range(-1 * moveDirRandomRange.y, moveDirRandomRange.y));
    }

    void FixedUpdate()
    {
        transform.Translate(moveDir * globalSpeed * Time.fixedDeltaTime); 
    }
}
