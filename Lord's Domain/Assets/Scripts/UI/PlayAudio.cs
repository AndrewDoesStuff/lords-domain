using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public GameObject button_highlight_audio;
    public GameObject button_click_audio;

    public void Drop_Highlight_Audio()
    {
        Instantiate(button_highlight_audio,transform.position,transform.rotation);
    }
    
    public void Drop_Click_Audio()
    {
        Instantiate(button_click_audio,transform.position,transform.rotation);
    }

}
