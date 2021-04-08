using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public float timer = 30;
    void Update()
    {
        if(timer > 0) {
            timer -= Time.deltaTime;
        }        
        else {
            Debug.Log("You Win!");
            SceneManager.LoadScene("DemoStart"); // resets the game, but we can change this in the future
        }
        // Debug.Log("Current Time: " + (int) timer);
    }
}
