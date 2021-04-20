using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    public AudioSource audio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audio.Play(0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        audio.Stop();
    }
}
