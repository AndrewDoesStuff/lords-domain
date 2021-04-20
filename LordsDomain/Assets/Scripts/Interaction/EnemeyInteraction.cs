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
            //Remember upon completion, loads previous save state
            GameObject.Find("MaleMC").GetComponent<PlayerStatus>().SavePlayerStatus();
            //SceneManager.LoadScene("Battle");
            Debug.Log("E Pressed");
        }
    }

}
