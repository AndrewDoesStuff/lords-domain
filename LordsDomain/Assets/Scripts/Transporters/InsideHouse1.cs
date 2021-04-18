using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouse1 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            collision.transform.position = new Vector3((float)159.287, (float)-9.22);
        }
    }
}
