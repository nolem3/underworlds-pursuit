using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCollide : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float propelVelocity;
    private AHealth myHealth;
    [SerializeField] private int lavaDamage = -1;
    private float lavaInvulnerabilityTime = 0.5f;
    private bool lavaInvulnerability;

    private void Start()
    {
        myHealth = GetComponent<AHealth>();
        if (myHealth == null) Debug.LogError("LavaCollide component is on a GameObject without an AHealth component!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (lavaInvulnerability) return;
        if (other.tag == "Lava")
        {
            Debug.Log("Hit lava");
            rb.velocity = new Vector2(rb.velocity.x, propelVelocity);
            myHealth.ChangeHealth(lavaDamage);
            StartCoroutine("LavaInvulnerabilityDelay");
        }
    }

    private IEnumerator LavaInvulnerabilityDelay()
    {
        lavaInvulnerability = true;
        yield return new WaitForSeconds(lavaInvulnerabilityTime);
        lavaInvulnerability = false;
    }
}
