using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : AHealthTracker
{
    [SerializeField] private Slider healthSlider;

    public override void HealthChanged(AHealth healthScript)
    {
        healthSlider.maxValue = healthScript.GetMaxHealth(); 
        healthSlider.value = healthScript.GetHealth(); 
    }
}
