using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStomp : MonoBehaviour
{
    // Bounce effect multiplier for altered jump height
    public float bounce;
    // Player bottom hitbox for enemy collision detection
    public Rigidbody2D rb2d;
    // Completion particle effect
    public ParticleSystem sparks;
   
    /**
     * Detect enemy sprite entrance into player sprite foot hitbox and contact with level complete flag
     * @param other: Collider of other object contacting player hitbox
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        int additionalPoints = 0;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            rb2d.velocity = new Vector2(rb2d.velocity.x, bounce);
            //ScoreManager.scoreTotal.ChangePoints(50);
            ScoreManager.scoreValue += 100;

        }
        else if (other.gameObject.name == "High Point")
        {
            // Bonus points and cool sprite effect if flag top is collided with
            // Calculate additional points depending on time remaining
            additionalPoints = FindObjectOfType<TimerCountdown>().secondsLeft * 100;
            // Cool sprite effects!
            // Add extra points to score value
            ScoreManager.scoreValue += additionalPoints;
            sparks.Play();
            FindObjectOfType<GameManager>().YouWin();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            additionalPoints = FindObjectOfType<TimerCountdown>().secondsLeft * 50;
            // Cool sprite effects!
            // Add extra points to score value
            if(ScoreManager.scoreValue < 8000)
            {
                ScoreManager.scoreValue += additionalPoints;
            }
            sparks.Play();
            FindObjectOfType<GameManager>().YouWin();
        }
        
    }
}