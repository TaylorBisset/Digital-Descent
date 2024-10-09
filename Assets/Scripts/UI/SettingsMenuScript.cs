using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
    public GameObject settingsButton;   // References the Settings Button
    public GameObject settingsMenu; // References the Settings Menu

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);   // Shows the Settings Menu
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);  // Hides the Settings Menu
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);  // Returns to Main Menu screen
    }
}
