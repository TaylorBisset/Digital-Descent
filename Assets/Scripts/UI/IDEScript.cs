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
    private Dictionary<string, object> variables = new Dictionary<string, object>();

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


    // - - - - - Code Execution - - - - - //


    public void ExecuteCode()
    {
        // Get text from the input field
        string[] userInputLines = inputField.text.Trim().Split('\n');   // split input into lines
        string output = ""; // Initialize an empty string for output

        // Process each line of input
        foreach (string line in userInputLines)
        {
            if (string.IsNullOrEmpty(line))
            {
            continue; // skip empty lines
            }

            // process lines and combine for output
            string result = ProcessLine(line);
            output += result +"\n";
        }

        // output to output text field
        outputText.text = output.Trim();
    }

    // - - - - - ExecuteCode Helper Methods - - - - - //

    private string ProcessLine(string line)
    {
        // check for print()
        if (line.Contains("="))
        {
            return HandleVariableAssignment(line);
        }

        else if (line.StartsWith("print(") && line.EndsWith(")"))
        {
            return HandlePrintCommand(line);
        }

        else
        {
            return "Error: Invalid format or command.\n";
        }
    }

    private string HandleVariableAssignment(string line)
    {
        // check for variables
        string[] parts = line.Split('=');   // create dictionary for variable assignment
        
        if (parts.Length != 2)
        {
            return "Error: Invalid assignment format.\n";
        }

        string variableName = parts[0].Trim(); // variable name, trim spaces
        string variableValue = parts[1].Trim(); // variable value, trim spaces

        // check if value is a string
        if (variableValue.StartsWith("\"") && variableValue.EndsWith("\""))
        {
            variableValue = variableValue.Trim('\"'); // remove quotes
            variables[variableName] = variableValue; // store variable in dictionary
            return $"Variable \"{variableName}\" set to \"{variableValue}\".";
        }

        // check if value is a number
        else if (float.TryParse(variableValue, out float numberValue))
        {
            variables[variableName] = numberValue;
            return $"Variable \"{variableName}\" set to \"{numberValue}\".";
        }

        // Error         
        else
        {
            return $"Error: Invalid value for variable \"{variableName}\".\n";
        }
    }

    private string HandlePrintCommand(string line)
    {
        // Extract the content inside the print() command
        string content = line.Substring(6, line.Length - 7).Trim(); // Trim the starting 'print(' and the ending ')'

        if (content.StartsWith("\"") && content.EndsWith("\"")) // Check for string literal
        {
            return content.Trim('\"') + "\n";    // Output the string directly by removing the quotes
        }

        else if (variables.ContainsKey(content))
        {
            return variables[content] + "\n"; // Output variable value
        }

        else
        {
            return $"Error: Undefined variable \"{content}\".\n";
        }
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
