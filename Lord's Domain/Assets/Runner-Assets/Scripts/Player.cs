using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D rb;
    public int health = 3;
    private Vector2 movement;
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(health == 0) {
            Debug.Log("You died!");
            SceneManager.LoadScene("Runner-MG");
        }        
    }
    void FixedUpdate() { 
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime); 
    }

}
