using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public TMP_InputField nameField;
    public void startGame(string level)
    {
        string name = nameField.text;
        if (name != "" && !name.Contains(" "))
        {
            StaticVariables.playerName = name;
            SceneManager.LoadScene(level);
        }
        else
        {
            nameField.text = "";
            //nameField.placeholder.GetComponent<Text>().text = "Name illegal.";
        }
    }
}