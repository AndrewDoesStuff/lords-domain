using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private float speed = 7.5f;
    public Rigidbody2D rb;
    public int health = 3;
    public GameObject gameOver;

    private Vector2 movement;
    private float oldSpeed;
    private int savedHealth;

    void Start() {
        oldSpeed = speed;
        savedHealth = health;
    }
    void Update()
    {
        if(health <= 0) {
            health = savedHealth;
            gameOver.SetActive(true);
            Destroy(gameObject);
            
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Debug.Log(health);
    }
    void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            speed += 5;
        }
        else {
            speed = oldSpeed;
        }
        Debug.Log(speed); 
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
               
    }

}
