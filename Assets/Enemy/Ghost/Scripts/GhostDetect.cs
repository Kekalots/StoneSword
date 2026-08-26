using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetect : MonoBehaviour
{
    private EnemyBaseAttack parentAttack;

    void Awake()
    {
        parentAttack = GetComponentInParent<EnemyBaseAttack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentAttack.OnPlayerEnterDetect(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentAttack.OnPlayerExitDetect();
        }
    }
}
