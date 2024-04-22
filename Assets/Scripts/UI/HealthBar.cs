using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
    }

    public void ModulateFill(float pct) {
        slider.value = pct;
    }
}
