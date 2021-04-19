using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed = 7.5f;
    private Rigidbody2D rb;
    public int health = 5;
    private Vector2 movement;
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(health == 0) {
            Debug.Log("You died!");
            SceneManager.LoadScene("Battle");
        }        
    }
    void FixedUpdate() { 
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); 
    }

}
