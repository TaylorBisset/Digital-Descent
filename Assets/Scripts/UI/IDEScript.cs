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

    // dictionary to hold variables
    private Dictionary<string, string> variables = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Start()
    {
        idePanel.SetActive(false);
    }

    public void OpenIDE()
    {
        idePanel.SetActive(true);   // Shows the Settings Menu
    }

    // - - - - - Banner - - - - - //

    public void CloseIDE() 
    {
        idePanel.SetActive(false);  // Hides the Settings Menu
    }

    public void ClearButton()
    {
        inputField.text = string.Empty;
    }

    public void ExecuteCode()
    {
        // Get text from the input field
        string[] userInputLines = inputField.text.Trim().Split('\n');   // split input into lines
        string output = ""; // Initialize an empty string for output

        // Process each line of input
        foreach (string line in userInputLines)
        {
            // check for variables
            if (line.Contains("=")) // handle variable assignment
            {
                string[] parts = line.Split('=');   // create dictionary for variable assignment
                if (parts.Length == 2)
                {
                    string variableName = parts[0].Trim(); // variable name, trim spaces
                    string variableValue = parts[1].Trim(); // variable value, trim spaces

                    // check if value is a string
                    if (variableValue.StartsWith("\"") && variableValue.EndsWith("\""))
                    {
                        variableValue = variableValue.Trim('\"'); // remove quotes
                        variables[variableName] = variableValue; // store variable in dictionary
                    }
                    else
                    {
                        output += $"Error: Invalid value for variable \"{variableName}\".\n";
                    }
                }
                else
                {
                    output += "Error: Invalid assignment format.\n";
                }
            }

            // check for print()
            else if (line.StartsWith("print(") && line.EndsWith(")"))    // check for print()
            {
                // Extract the content inside the print() command
                string content = line.Substring(6, line.Length - 7).Trim(); // Trim the starting 'print(' and the ending ')'

                if (content.StartsWith("\"") && content.EndsWith("\"")) // Check for string literal
                {
                    output += content.Trim('\"') + "\n";    // Output the string directly by removing the quotes
                }

                else if (variables.ContainsKey(content))
                {
                    output += variables[content] + "\n"; // Output variable value
                }

                else
                {
                    output += $"Error: Undefined variable \"{content}\".\n";
                }
            }

            else
            {
                output += "Error: Invalid format or command.\n";
            }
        }

        // output to output text field
        outputText.text = output.Trim();
    }

    // - - - - - Library - - - - - //

    public void PrintFunctionButton()
    {
        // when button is pressed, print function is added to the user input field
        inputField.text += "\nprint(\"Hello, World!\")";

    }

    public void PrintFunctionVariableButton()
    {
        // when button is pressed, print function is added to the user input field
        inputField.text += "\nmessage = \"Hello, World!\"\nprint(message)";
        
    }

}
