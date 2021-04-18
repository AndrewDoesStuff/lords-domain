using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRoom4 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            collision.transform.position = new Vector3((float)263, (float)-28.2);
        }
    }
}
