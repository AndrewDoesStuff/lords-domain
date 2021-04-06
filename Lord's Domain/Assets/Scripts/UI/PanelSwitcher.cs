using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject objectPanel;

    public void SwitchPanel()
    {
        if (objectPanel != null)
        {
            objectPanel.SetActive(true);
            currentPanel.SetActive(false);
        }
    }
}
