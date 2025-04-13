using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 60f; // Set in the Inspector
    public TMP_Text timerText;
    public AudioSource timeoutAudio; // Sound to play on timeout
    public AudioSource tickTockAudio; // Sound to play in last 10 seconds
    public string mainMenuSceneName = "MainMenu"; // Replace with your actual menu scene name

    private float currentTime;
    private bool timerRunning = true;
    private bool tickTockStarted = false;

    void Start()
    {
        currentTime = timeLimit;
        UpdateTimerUI();
    }

    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            // Start tick-tock at 10 seconds remaining
            if (!tickTockStarted && currentTime <= 10f)
            {
                tickTockStarted = true;
                if (tickTockAudio != null && !tickTockAudio.isPlaying)
                    tickTockAudio.Play();
            }

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                StartCoroutine(HandleTimeout());
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    IEnumerator HandleTimeout()
    {
        if (tickTockAudio != null && tickTockAudio.isPlaying)
            tickTockAudio.Stop();

        if (timeoutAudio != null)
            timeoutAudio.Play();

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
