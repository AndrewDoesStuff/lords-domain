using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInsideCastle : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            collision.transform.position = new Vector3((float)0, (float)0);
        }
    }
}
