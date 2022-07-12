using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Restart the game and reload the scene upon an 'R' keypress
public class Restart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Reset score with newly loaded scene
            ScoreManager.scoreValue = 0;
            // Reset lives with fresh value
            PlayerDeath.numLives = 3;
        } else if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
