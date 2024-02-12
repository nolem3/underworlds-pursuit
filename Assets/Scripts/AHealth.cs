using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void ChangeHealth(int change)
    {
        health = Mathf.Clamp(health + change, 0, maxHealth); 
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
