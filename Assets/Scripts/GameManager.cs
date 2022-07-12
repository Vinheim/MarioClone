using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public YouWin winScreen;


    // Display the UI game over screen to provide restart and quit functionality
    public void GameOver()
    {
        // Check for perceived negative score and set to 0 for display if necessary
        if (ScoreManager.scoreValue < 0)
            gameOverScreen.Setup(0);
        else 
            gameOverScreen.Setup(ScoreManager.scoreValue);
    }

    public void YouWin()
    {
        if (ScoreManager.scoreValue < 0)
            winScreen.Setup(0);
        else
            winScreen.Setup(ScoreManager.scoreValue);
        
    }
}
