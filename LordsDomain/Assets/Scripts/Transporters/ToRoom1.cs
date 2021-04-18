using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRoom1 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            collision.transform.position = new Vector3((float)154.03, (float)-28.5);
        }
    }
}
