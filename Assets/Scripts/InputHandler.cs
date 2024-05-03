using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;
    [SerializeField] Button Play; // Reference to the play button

    private void Start()
    {
        // Disable the play button at the start
        Play.interactable = false;
    }

    public void ValidateInput()
    {
        string input = inputField.text;

        if (int.TryParse(input, out int age))
        {
            // Check if age is greater than or equal to 12
            if (age >= 12)
            {
                resultText.text = "Valid input";
                resultText.color = Color.green;

                // Enable the play button
                Play.interactable = true;
            }
            else
            {
                resultText.text = "You must be at least 12 years old to play.";
                resultText.color = Color.red;

                // Disable the play button
                Play.interactable = false;
            }
        }
        else
        {
            resultText.text = "Invalid input";
            resultText.color = Color.red;

            // Disable the play button
            Play.interactable = false;
        }
    }
}
