using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDEScript : MonoBehaviour
{
    public GameObject ideButton;   // References the Settings Button
    public GameObject idePanel; // References the Settings Menu

    // Start is called before the first frame update
    void Start()
    {
        idePanel.SetActive(false);
    }

    public void OpenIDE()
    {
        idePanel.SetActive(true);   // Shows the Settings Menu
    }

    public void CloseIDE()
    {
        idePanel.SetActive(false);  // Hides the Settings Menu
    }
}
