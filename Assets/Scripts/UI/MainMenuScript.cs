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
        
    }

    public void QuitGame()
    {
        Application.Quit(); // Exits the game
    }
}
