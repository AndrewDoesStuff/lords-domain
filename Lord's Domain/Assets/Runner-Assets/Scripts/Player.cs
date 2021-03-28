using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7.5f;
    public int health = 3;
    private Vector2 movement;
    public int movementInc = 5;
    private float maxH;
    private float minH;
    void Start() {
        minH = movementInc * -1;
        maxH = movementInc; 
        // this is so it only goes up 3 times
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movement, speed * Time.deltaTime);
        Debug.Log(transform.position.ToString());
        if(Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < maxH) { // we can change this in the future
            movement = new Vector2(transform.position.x, transform.position.y + movementInc);

        }        
        else if(Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > minH) { // we can change this in the future
            movement = new Vector2(transform.position.x, transform.position.y - movementInc);
        }
    }
}
