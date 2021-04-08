using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemeyInteraction : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Entered Trigger");
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("Battle");
            Debug.Log("E Pressed");
        }
    }

}
