using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIHandler : MonoBehaviour
{
    private Animal animal;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider thirstSlider;

    private void Start() {
        animal = GetComponentInParent<Animal>();
    }

    private void Update() {
        if (animal != null) {
            //Update stats
            healthSlider.value = animal.health;
            thirstSlider.value = animal.thirst;
            hungerSlider.value = animal.hunger;
        }
    }
}
