using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovePrototype : MonoBehaviour
{
    public int playerSpeed = 10;
    public bool facingRight = false;
    public int playerJumpPower = 150;
    public float moveX;
    public float moveY;

    // Start is called before the first frame update
    //void Start()

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }

    void playerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown ("Jump")) {
            jump();
        }

        //ANIMATIONS
        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == false) {
            flipPlayer();
        }

        else if (moveX > 0.0f && facingRight == true) {
            flipPlayer();
        }
        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void jump () {
        //Jump Code
        GetComponent<Rigidbody2D>().AddForce (Vector2.up * playerJumpPower);

    }

    void flipPlayer () {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }
}
