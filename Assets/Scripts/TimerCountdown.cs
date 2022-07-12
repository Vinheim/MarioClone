using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 85;
    public bool takingAway = false;
    bool isPaused = false;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = secondsLeft.ToString();
    }

    void Update()
    {
        if (takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        } else if (secondsLeft == 0) {
            // Use additional buffer time in condition to allow for processing of EndGame()
            FindObjectOfType<GameManager>().GameOver();
            FindObjectOfType<AudioManager>().BGM.Pause();
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = secondsLeft.ToString();
        takingAway = false;
    }

    // Pause/unpause timer, gameplay, and music depending on use keypress and game completion
    public void PauseTimer()
    {
        if (Input.GetKeyDown("p"))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                FindObjectOfType<AudioManager>().BGM.Pause();
            }
            else
            {
                FindObjectOfType<AudioManager>().BGM.Play();
                Time.timeScale = 1;
                isPaused = false;
            }
        }
    }
}
