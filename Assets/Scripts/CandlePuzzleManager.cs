using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
    public List<string> correctOrder = new List<string> { "Green", "Red", "Yellow", "Blue" };
    private List<string> playerOrder = new List<string>();
    public List<Candle> allCandles; // Drag candles here in Inspector

    public AudioSource successAudio;
    public AudioSource failAudio;

    private bool isChecking = false;

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
        yield return new WaitForSeconds(1f); // 1-second suspense pause

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
            // Let candles stay on
        }
        else
        {
            failAudio.Play();
            yield return new WaitForSeconds(0.5f); // slight buffer before turning off
            ResetPuzzle();
        }

        isChecking = false;
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
