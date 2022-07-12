using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public Transform respawnPoint;
    public static int numLives = 3;
    public AudioClip deathTrack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            transform.position = respawnPoint.position;
            ScoreManager.scoreValue -= 400;
            numLives--;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = respawnPoint.position;
            ScoreManager.scoreValue -= 200;
            numLives--;
        }

        if (numLives == 0)
        {
            Debug.Log("lol you died lol lol you died");
            // FindObjectOfType<GameManager>().EndGame();

            // Reset game through game manager upon loss of all lives
            FindObjectOfType<GameManager>().GameOver();
            FindObjectOfType<AudioManager>().ChangeMusic(deathTrack);
        }
    }
}