using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : MonoBehaviour
{

    [SerializeField]
    private float walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + walkSpeed, transform.position.y, transform.position.z);
        //Debug.Log(transform.position.x + walkSpeed);
    }
}
