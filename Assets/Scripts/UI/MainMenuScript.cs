// MainMenuScript.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu; // References the MainMenuObject
    public GameObject levelSelectMenu;  // References the LevelSelectMenuObject

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);   // Show Main Menu
        levelSelectMenu.SetActive(false);   // Hide Level Select Menu
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);  // Hide Main Menu
        levelSelectMenu.SetActive(true);    // Show Level Select Menu
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); // Load level based on index: currently, levels are same as index, as main menu scene is indexed at 0
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);   // Show Main Menu
        levelSelectMenu.SetActive(false);   // Hide Level Select Menu
    }

    public void QuitGame()
    {
        Application.Quit(); // Exits the game
    }
}
