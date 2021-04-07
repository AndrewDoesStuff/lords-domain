using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownDialogue : MonoBehaviour
{
    public GameObject storeInterface;
    private bool dialogue_flag = false;
    private GameObject closet_ob;
    private float closet_distance;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((GameObject.Find("Store").transform.position - transform.position).sqrMagnitude < 3)
            {
                storeInterface.SetActive(true);
            }
            else
            {
                if (!dialogue_flag)
                {
                    (closet_ob, closet_distance) = FindClosestEnemy();
                    Debug.Log("Distance:" + closet_distance);
                    if (closet_distance < 3 && closet_ob.GetComponent<DialogueTrigger>() != null)
                    {
                        DialogueTrigger script1 = closet_ob.GetComponent<DialogueTrigger>();
                        script1.TriggerDialogue();
                        dialogue_flag = true;
                    }
                }
                else
                {
                    DialogueManager script2 = GetComponent<DialogueManager>();
                    if (script2.DisplayNextSentence())
                    {
                        dialogue_flag = false;
                    }
                }
            }
        }
    }
    
    public (GameObject, float) FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return (closest, distance);
    }

    public void disableStoreInterface()
    {
        storeInterface.SetActive(false);
    }
}
