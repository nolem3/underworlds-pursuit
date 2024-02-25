using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollide : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float propelVelocity;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lava")
        {
            Debug.Log("Hit lava");
            rb.velocity = new Vector2(rb.velocity.x, propelVelocity);
            // LOSE HEALTH
        }
    }
}
