using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D rb;
    public int health = 3;
    private Vector2 movement;
    private float oldSpeed;
    private bool isBoosting;
    void Start() {
        oldSpeed = speed;
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // none of this works lol
        // Debug.Log(health);
        // if(Input.GetKeyDown(KeyCode.LeftShift)) {
        //     isBoosting = true;
        // }
        // else if(Input.GetKeyUp(KeyCode.LeftShift)) {
        //     isBoosting = false;
        // }
        // if(isBoosting) {
        //     speed += 3; // how do i get this to run once and not every frame
        // }
        // else {
        //     speed = oldSpeed;
        // }
    }
    void FixedUpdate() { 
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); 
    }

}
