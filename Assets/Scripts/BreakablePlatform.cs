using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private GameObject breakPrefab;
    [Header("Position Causes Break: ")]
    [SerializeField] private bool xPosNegativeCausesBreak = false;
    [SerializeField] private float xPosMin = -8;
    [Header("Collisions Cause Break: ")]
    [SerializeField] private bool normalCollisions = true;
    [SerializeField] private bool triggerCollisions = false;
    [SerializeField] private List<string> tagsToBreak = new List<string>();
    [SerializeField] private List<string> layersToBreak = new List<string>();

    private void Update()
    {
        if (xPosNegativeCausesBreak && transform.position.x <= xPosMin) Break();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (normalCollisions) TryToBreak(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerCollisions) TryToBreak(other.gameObject);
    }

    private void TryToBreak(GameObject otherObject)
    {
        if (tagsToBreak.Contains(otherObject.tag) || layersToBreak.Contains(LayerMask.LayerToName(otherObject.layer)))
        {
            Break();
        }
    }

    public void Break()
    {
        Instantiate(breakPrefab, transform.position, breakPrefab.transform.rotation, null);
        Destroy(gameObject);
    }
}
