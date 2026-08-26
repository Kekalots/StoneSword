using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider enemyHPBar;

    private void Update()
    {
        if (enemyHealth == null) return;
        enemyHPBar.value = enemyHealth.currentHP / enemyHealth.enemyInfo.maxHP;
    }
}

