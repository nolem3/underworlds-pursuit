using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSlider : AHealthTracker
{
    [SerializeField] private Slider healthSlider;

    public override void HealthChanged(AHealth healthScript)
    {
        healthSlider.maxValue = healthScript.GetMaxHealth(); 
        healthSlider.value = healthScript.GetHealth();

        // Check for player death:
        if (healthSlider.value <= 0 && gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player dead");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } 
    }
}
