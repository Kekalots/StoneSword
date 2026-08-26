using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private bool isAttackingLock;
    public Collider2D hitRange;
    private void Awake()
    {
        anim=GetComponent<Animator>();
        isAttackingLock = false;
        hitRange.enabled = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)&&isAttackingLock==false)
        {
            isAttackingLock = true;
            hitRange.enabled = true;
            anim.SetTrigger("isAttacking");
        }
    }
    public void OnAttackFinished()
    {
        isAttackingLock= false;
        hitRange.enabled = false;
    }
}
