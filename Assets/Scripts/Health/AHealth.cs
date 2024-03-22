using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private List<AHealthTracker> healthTrackers = new List<AHealthTracker>();
    [SerializeField] private float invulnerabilityTime;
    private float lastHitTime;
    private int health = 999999;
    private float healthRatio = 1;

    private void Start()
    {
        health = maxHealth;
        healthRatio = 1.0f;
    }

    public void ChangeHealth(int change)
    {
        if (invulnerabilityTime > 0 && Time.time <= lastHitTime + invulnerabilityTime) return;
        health = Mathf.Clamp(health + change, 0, maxHealth);
        healthRatio = (float)((float)health / (float)maxHealth);
        if (change < 0) lastHitTime = Time.time;
        foreach (AHealthTracker tracker in healthTrackers) tracker.HealthChanged(this);
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealthRatio()
    {
        return healthRatio;
    }
}
