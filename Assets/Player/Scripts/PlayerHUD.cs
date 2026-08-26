using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider slider;
    public PlayerHealth playerHealth;
    public PlayerNow playerNow;
    private void Update()
    {
        slider.value = playerHealth.currentHP / playerNow.maxHP;
    }
}
