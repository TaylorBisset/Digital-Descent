using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu; // References the MainMenuObject
    public GameObject levelSelectMenu;  // References the LevelSelectMenuObject

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that Main Menu is active upon game starting
        // Ensure that Level Select Menu is hidden when game sarts
        mainMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
    }

    public void PlayGame()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit(); // Exits the game
    }
}
