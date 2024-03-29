using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildParent : MonoBehaviour
{
    [SerializeField] private bool normalCollisions = true;
    [SerializeField] private bool triggerCollisions = false;
    [SerializeField] private List<string> tagsToCollect = new List<string>();
    [SerializeField] private List<string> layersToCollect = new List<string>();
    private List<Transform> collectedChildren = new List<Transform>();
    [SerializeField] private bool dontCollectPositiveXInputPlayer = false;
    [SerializeField] private bool dontCollectPositiveYVelocityPlayer = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (normalCollisions) TryToCollect(collision.gameObject.transform); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerCollisions) TryToCollect(other.gameObject.transform);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (normalCollisions) TryToCollect(collision.gameObject.transform);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (triggerCollisions) TryToCollect(other.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (normalCollisions) TryToUncollect(collision.gameObject.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerCollisions) TryToUncollect(other.gameObject.transform);
    }

    private void TryToCollect(Transform given)
    {
        if (dontCollectPositiveYVelocityPlayer)
        {
            PlayerMove playerMove = given.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                if (playerMove.GetVelocity().y > 0.1f)
                {
                    if (collectedChildren.Contains(given))
                    {
                        given.SetParent(null);
                        collectedChildren.Remove(given);
                    }
                    return;
                }
            }
        }

        if (dontCollectPositiveXInputPlayer)
        {
            PlayerMove playerMove = given.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                //if (Mathf.Abs(playerMove.CurrentMoveInput()) > 0.1f)
                if (playerMove.CurrentMoveInput() > 0.1f)
                {
                    if (collectedChildren.Contains(given))
                    {
                        given.SetParent(null);
                        collectedChildren.Remove(given);
                    }
                    return;
                }
            }
        }

        // if (collectedChildren.Contains(given)) return;
        if (collectedChildren.Contains(given) && given.parent != null) return;
        foreach (string tag in tagsToCollect)
        {
            if (given.CompareTag(tag))
            {
                given.SetParent(this.gameObject.transform);
                collectedChildren.Add(given);
                return;
            }
        }
        foreach (string layer in layersToCollect)
        {
            if (given.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                given.SetParent(this.gameObject.transform);
                collectedChildren.Add(given);
                return;
            }
        }
    }

    private void TryToUncollect(Transform given)
    {
        if (!collectedChildren.Contains(given)) return;

        if (dontCollectPositiveYVelocityPlayer)
        {
            PlayerMove playerMove = given.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                if (playerMove.GetVelocity().y > 0.0f)
                {
                    given.SetParent(null);
                    collectedChildren.Remove(given);
                }
            }
        }

        if (dontCollectPositiveXInputPlayer)
        {
            PlayerMove playerMove = given.GetComponent<PlayerMove>();
            if (playerMove != null)
            {
                //if (Mathf.Abs(playerMove.CurrentMoveInput()) > 0.1f)
                if (playerMove.CurrentMoveInput() > 0.1f)
                {
                    given.SetParent(null);
                    collectedChildren.Remove(given);
                }
            }
        }

        foreach (string tag in tagsToCollect)
        {
            if (given.gameObject.CompareTag(tag))
            {
                given.SetParent(null);
                collectedChildren.Remove(given);
                return;
            }
        }
        foreach (string layer in layersToCollect)
        {
            if (given.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                given.SetParent(null);
                collectedChildren.Remove(given);
                return;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Transform t in collectedChildren)
        {
            t.SetParent(null);
        }
    }
}

