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

    }

    private void FixedUpdate()
    {
        //Movement
        rigidBody.MovePosition(rigidBody.position + dir * runSpeed * Time.fixedDeltaTime);
    }
}
