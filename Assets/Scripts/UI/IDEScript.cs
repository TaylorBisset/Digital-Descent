// IDEScript.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    // add maximum execution time
    private float globalTimeout = 5f; // 5 seconds
    private float startTime; // time when execution starts

    public void ExecuteCode()
    {
        // start execution timer
        startTime = Time.time;

        // check for empty input
        if (string.IsNullOrEmpty(outputText.text))
        {
            outputText.text = "No code to execute.";
            return;
        }

        // Get text from the input field
        string[] userInputLines = inputField.text.Trim().Split('\n');   // split input into lines
        StringBuilder output = new StringBuilder(); // Initialize new StringBuilder instance to construct outputs

        // use `while` loop to account for multi-line constructions
        int i = 0;
        while (i < userInputLines.Length)
        {
            string line = userInputLines[i].Trim(); // trim leading or trailing whitespace

            if (string.IsNullOrEmpty(line))
            {
                continue; // skip empty lines
            }

            // check for while loop
            if (line.StartsWith("while"))
            {
                // extract conditon of loop
                string condition = line.Substring(6).Trim(); // remove 'while' from line
                // collect body of loop
                List<string> loopBody = new List<string>();
                // move to next line
                i++;

                // loop through to the end of the `while` loop body
                while (i < userInputLines.Length && !string.IsNullOrEmpty(userInputLines[i]))
                {
                    loopBody.Add(userInputLines[i].Trim()); // Add line to loop body
                    i++;
                }

                // execute the `while` loop
                string loopOutput = HandleWhileLoop(condition, loopBody); 
                output.AppendLine(loopOutput);
            }

            else
            {
                // if not in a loop, process as single-line command
                string result = ProcessLine(line);
                output.AppendLine(result);
                i++;
            }

            // check for timeout
            if (Time.time - startTime > globalTimeout)
            {
                outputText.text = "Error: Execution timeout exceeded.";
                return;
            }
        }
            // output to output text field
            outputText.text = output.ToString().Trim();
    }

    // - - - - - ExecuteCode Helper Methods - - - - - //

    private string ProcessLine(string line)
    {
        // check for variable
        if (line.Contains("="))
        {
            return HandleVariableAssignment(line);
        }

        else if (line.Contains("+") || line.Contains("+") || line.Contains("+") || line.Contains("+"))
        {
            return HandleArithmatic(line);
        }

        // check for print()
        else if (line.StartsWith("print(") && line.EndsWith(")"))
        {
            return HandlePrintCommand(line);
        }

        else
        {
            return "Error: Invalid format or command.\n";
        }
    }

    private string HandleWhileLoop(string condition, List<string> loopBody)
    {
        int maxIterations = 500; // Prevent infinite loop
        int iterationCount = 0;

        StringBuilder output = new StringBuilder();

        // continue executing the loop until the condition is met
        while (EvaluateCondition(condition))
        {
            // check if maximum iterations have been reached
            if (iterationCount++ > maxIterations)
            {
                return "Error: Infinite loop detected.";
            }

            foreach (string line in loopBody)
            {
                string result = ProcessLine(line);
                // execute loop and append output
                output.AppendLine(result);

                // check for timeout duration
                if (Time.time - startTime > globalTimeout)
                {
                    return "Error: Execution timeout exceeded.";
                }

                // check if loop condition has changed to prevent infinite loops
                if (!EvaluateCondition(condition))
                {
                    break;
                }
            }
        }
        return output.ToString().Trim(); // return the accumulated output from loop
    }

    private bool EvaluateCondition(string condition)
    {
        // working example:
        // while (i < 5)
        string[] parts = condition.Split(' ');

        if (parts.Length != 3)
        {
            throw new System.Exception("Invalid condition format.");
        }

        string variableName = parts[0]; // i
        string operatorType = parts[1]; // <
        string value = parts[2];        // 5

        // get variable value for comparison
        float variableValue = Convert.ToSingle(variables[variableName]); // variable value
        float compareValue = float.Parse(value); // value to compare agaisnt

        // perform comparison based on the operator
        switch (operatorType)
        {
            case "<":
                return variableValue < compareValue;
            case "<=":
                return variableValue <= compareValue;
            
            case ">":
                return variableValue > compareValue;
            case ">=":
                return variableValue >= compareValue;
           
            case "==":
                return variableValue == compareValue;
            case "!=":
                return variableValue != compareValue;
            default:
                // if not recognized, throw error
                throw new System.Exception($"Invalid operator: {operatorType}");
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


    // - - - - - Arithmatic Operations - - - - - //


    private string HandleArithmatic(string line)
    {
        // read the operator, split from line
        string[] tokens = line.Split(new char[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
        
        // check if exactly two operands
        if (tokens.Length == 2)
        {
            string var1 = tokens[0].Trim();
            string var2 = tokens[1].Trim();

            // check if variables exist
            if (variables.ContainsKey(var1) && variables.ContainsKey(var2))
            {
                // if operands are variables, convert to their assigned values
                float val1 = Convert.ToSingle(variables[var1]);
                float val2 = Convert.ToSingle(variables[var2]);

                // perform arithmatic based on the operator
                if (line.Contains("+"))
                {
                    return (val1 + val2).ToString(); // Addition
                }

                else if (line.Contains("-"))
                {
                    return (val1 - val2).ToString(); // Subtraction
                }

                else if (line.Contains('*'))
                {
                    return (val1 * val2).ToString(); // Multiplication
                }

                else if (line.Contains('/'))
                {
                    return (val1 / val2).ToString(); // Division
                }
            }
        }

        return "Error: Invalid arithmatic operation.";
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


/*
Script Layout



*/