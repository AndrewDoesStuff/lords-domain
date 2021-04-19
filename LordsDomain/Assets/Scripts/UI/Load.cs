using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public GameObject name;
    public void loadGame(string level)
    {
        StaticVariables.playerName = name.GetComponent<Text>().text;
        SceneManager.LoadScene(level);
    }
}