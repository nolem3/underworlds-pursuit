using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShot : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Can change this to a better check that possibly uses a bullet tag:
        if (other.gameObject.name.Contains("PlayerShot"))
        {
            Debug.Log("Target Shot");
            Destroy(other.gameObject);
            // For now, destroy. Can maybe integrate health system for stationary targets in the future
            Destroy(this.gameObject);
        }
    }
}
