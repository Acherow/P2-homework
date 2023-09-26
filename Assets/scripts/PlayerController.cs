using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    Vector3 dir;
    public float speed;
    bool facingright;

    private void Start()
    {
        facingright = true;
        transform.localEulerAngles = new Vector3(1,1,1);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInput();
        HandleAnim();
    }

    private void FixedUpdate()
    {
        if(dir != Vector3.zero)
            Move();
    }

    void GetInput()
    {
        dir = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized;
    }
    void Move()
    {
        rb.AddForce(dir * speed * 10);
    }
    void HandleAnim()
    {
        if((facingright && dir.x <0) || (!facingright && dir.x >0))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,1,1);
            facingright = !facingright;
        }

        anim.SetBool("running", dir != Vector3.zero);
    }
}
