using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private bool normalCollisions = true; 
    [SerializeField] private bool triggerCollisions = false;
    [SerializeField] private List<string> tagsToDamage = new List<string>();
    [SerializeField] private List<string> layersToDamage = new List<string>();
    [SerializeField] private int healthChange = -1;
    [SerializeField] private bool destroyOnDamage = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (normalCollisions) TryToDamage(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerCollisions) TryToDamage(other.gameObject);
    }

    private void TryToDamage(GameObject otherObject) 
    {
        if (tagsToDamage.Contains(otherObject.tag) || layersToDamage.Contains(LayerMask.LayerToName(otherObject.layer)))
        {
            AHealth healthScript = otherObject.GetComponent<AHealth>();
            healthScript.ChangeHealth(healthChange);
            if (destroyOnDamage) Destroy(gameObject);
        }
    }
}
