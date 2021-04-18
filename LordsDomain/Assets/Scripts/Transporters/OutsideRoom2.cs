using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideRoom2 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            collision.transform.position = new Vector3((float)155.0, (float)-10.1);
        }
    }
}
