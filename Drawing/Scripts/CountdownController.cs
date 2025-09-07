using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    private Coroutine countdownCoroutine; // Store reference to the coroutine
    private bool isCountdownStopped = false; // Track if countdown is stopped

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0 && isCountdownStopped == false)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        countdownDisplay.text = countdownTime.ToString();
    }

    public void StopCountdown()
    {
        isCountdownStopped = true;
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
    }

}
