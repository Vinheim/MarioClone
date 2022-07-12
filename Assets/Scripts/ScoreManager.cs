using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;

    // Initialize score text component to display on canvas
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update text on screen to reflect score as changed
    void Update()
    {
        CheckScore();
        score.text = scoreValue.ToString();
    }

    // Check for negative score and reset to 0 if so
    void CheckScore()
    {
        // Prevent display of any negative scores with nonnegative check.
        if (scoreValue < 0)
        {
            scoreValue = 0;
        }
        else if (scoreValue >= 2000 && PlayerDeath.numLives < 3)
        {
            scoreValue -= 2000;
            PlayerDeath.numLives += 1;
        }
    }
}