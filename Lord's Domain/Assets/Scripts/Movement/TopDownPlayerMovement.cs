using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidBody;
    public float runSpeed = 10f;
    Vector2 dir;

    // Update is called once per frame
    void Update()
    {
        //Input
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (dir.x > 0)
        {
            animator.SetTrigger("GoRight");
        }
        
        if (dir.x < 0)
        {
            animator.SetTrigger("GoLeft");
        }
        
        if (dir.y > 0)
        {
            animator.SetTrigger("GoUp");
        }
        
        if (dir.y < 0)
        {
            animator.SetTrigger("Normal");
        }
    }

    private void FixedUpdate()
    {
        //Movement
        rigidBody.MovePosition(rigidBody.position + dir * runSpeed * Time.fixedDeltaTime);
    }
}
