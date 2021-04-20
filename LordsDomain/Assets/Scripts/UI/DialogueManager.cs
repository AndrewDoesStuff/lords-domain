using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Queue<string> sentences;
    public string sceneText;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Save State
        GameObject.Find("MaleMC").GetComponent<PlayerStatus>().SavePlayerStatus();

        animator.SetBool("IsOpen", true);
        
        nameText.text = dialogue.name;
        //Scene Changer
        sceneText = dialogue.scene;
        

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return true;
        }

        string sentence = sentences.Dequeue();
		sentence = sentence.Replace("@", StaticVariables.playerName);
        dialogueText.text = sentence;

        return false;
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        //Scene Changer
        if (sceneText != null)
        {
            Debug.Log(sceneText);
            SceneManager.LoadScene(sceneText);
        }
        
    }
}
