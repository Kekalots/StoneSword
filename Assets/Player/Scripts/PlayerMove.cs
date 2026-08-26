using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 1f;
    [SerializeField] private LayerMask raycastlayer1 = 1 << 6;
    [SerializeField] private LayerMask raycastlayer2 = 1 << 8;

    float input_x;
    bool isGround;
    Rigidbody2D rb;
    BoxCollider2D cld;
    Animator anim;
    Vector3 oscale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        oscale = transform.localScale;
    }

    void Update()
    {
        Jump();
        RayCast();
        MyAnimation();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void MyAnimation()
    {
        anim.SetBool("Moving", input_x != 0);
    }
    private void Move()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input_x * speed, rb.velocity.y);
        if (input_x > 0)
            transform.localScale = new Vector3(oscale.x, oscale.y, oscale.z);
        else if (input_x < 0)
            transform.localScale = new Vector3(-oscale.x, oscale.y, oscale.z);
    }

    private void Jump()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * 900 * jumpForce);
        }
    }

    private void RayCast()
    {
        Vector3 origin = cld.bounds.center + new Vector3(0, -cld.bounds.extents.y, 0);
        RaycastHit2D hit1 = Physics2D.Raycast(origin, Vector2.down, 0.1f, raycastlayer1);
        RaycastHit2D hit2 = Physics2D.Raycast(origin, Vector2.down, 0.1f, raycastlayer2);
        Debug.DrawLine(origin, origin + Vector3.down * 0.1f, Color.red);
        if (hit1.collider != null||hit2.collider!=null)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}
