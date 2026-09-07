using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;
    public PlayerNow playerNow;
    private void Awake()
    {
        if (playerHealth == null) playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerNow == null) playerNow = FindObjectOfType<PlayerNow>();
    }
    private void Update()
    {
        if (slider == null || playerHealth == null || playerNow == null) return;
        slider.value = playerHealth.currentHP / playerNow.maxHP;
    }
}