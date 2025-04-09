using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandlePuzzleManager : MonoBehaviour
{
    public List<string> correctOrder = new List<string> { "Green", "Red", "Yellow", "Blue" };
    private List<string> playerOrder = new List<string>();
    public List<Candle> allCandles;


    public AudioSource successAudio;
    public AudioSource failAudio;

    private bool isChecking = false;

    public List<Image> revealImages;

    private void Start()
    {
        // Disable all images at start
        foreach (var img in revealImages)
        {
            img.gameObject.SetActive(false);
        }
    }

    public void CandleLit(string color)
    {
        if (isChecking) return;

        playerOrder.Add(color);

        if (playerOrder.Count == correctOrder.Count)
        {
            isChecking = true;
            StartCoroutine(CheckPuzzle());
        }
    }

    private IEnumerator CheckPuzzle()
    {
        yield return new WaitForSeconds(1f); 
        bool correct = true;
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (playerOrder[i] != correctOrder[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            successAudio.Play();
            RevealImages();
        }
        else
        {
            failAudio.Play();
            yield return new WaitForSeconds(0.5f); 
            ResetPuzzle();
        }

        isChecking = false;
    }

    void RevealImages()
    {
        foreach (var img in revealImages)
        {
            img.gameObject.SetActive(true);
        }
    }

    void ResetPuzzle()
    {
        playerOrder.Clear();
        foreach (var candle in allCandles)
        {
            candle.Extinguish();
        }
    }
}
