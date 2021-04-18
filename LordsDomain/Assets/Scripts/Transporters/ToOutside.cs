using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToOutside : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector3((float)73.7, (float)-2.9);
    }
}
