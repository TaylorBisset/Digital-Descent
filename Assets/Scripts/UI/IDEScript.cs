using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IDEScript : MonoBehaviour
{
    public GameObject ideButton;   // References the Settings Button
    public GameObject idePanel; // References the Settings Menu
    public TMP_InputField inputField;   // Reference IDE Input Field
    public TMP_Text outputText;   // Reference IDE output text

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

    public void ExecuteCode()
    {
        // Get text from the input field
        string userInput = inputField.text;

        if (userInput.StartsWith("print(") && userInput.EndsWith(")"))  // check for print()
        {
            // Extract the content inside the print() command
            string content = userInput.Substring(6, userInput.Length - 7).Trim();   // Trim the starting 'print(' and the ending ')'

            if (content.StartsWith("\"") && content.EndsWith("\"")) // Check if the user's content is a string
            {
                outputText.text = content.Trim('\"'); // Output the string directly by removing the quotes
            }
        }
    }

}
