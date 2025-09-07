using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static bool isGameOver;
    public static bool isWin;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public CountdownController countdownController;
    private void Awake()
    {
        gameOverScreen.SetActive(false);
        isGameOver = false;
        winScreen.SetActive(false);
        isWin = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            if (countdownController != null)
            {
                countdownController.StopCountdown();
                // Debug.Log("Countdown Stopped");
            }

        }
        else if (isWin)
        {
            winScreen.SetActive(true);
            if (countdownController != null)
            {
                countdownController.StopCountdown();
                // Debug.Log("Countdown Stopped");
                Wave wave = FindObjectOfType<Wave>();
                if (wave != null)
                {
                    wave.StopWave(); 
                }
            }
        }
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnHome()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
