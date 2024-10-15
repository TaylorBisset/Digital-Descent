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
        string[] userInputLines = inputField.text.Trim().Split('\n');   // split input into lines
        string output = ""; // Initialize an empty string for output

        // Process each line of input
        foreach (string line in userInputLines) 
        {
            if (line.StartsWith("print(") && line.EndsWith(")"))    // check for print()
            {
                // Extract the content inside the print() command
                string content = line.Substring(6, line.Length - 7).Trim(); // Trim the starting 'print(' and the ending ')'

                if (content.StartsWith("\"") && content.EndsWith("\"")) // Check if the user's content is a string
                {
                    output += content.Trim('\"') + "\n";    // Output the string directly by removing the quotes
                }
            }
            else
            {
                output += "Error: Invalid format or command.\n";
            }
        }
        outputText.text = output.Trim();
    }

}
