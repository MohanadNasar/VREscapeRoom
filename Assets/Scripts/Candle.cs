using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Candle : MonoBehaviour
{
    public string candleColor; // Set in inspector: "Red", "Green", etc.
    public GameObject flame;
    public GameObject smoke;
    public Light pointLight;

    public AudioSource lightAudio;
    private bool isLit = false;

    public CandlePuzzleManager puzzleManager;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnCandleInteracted);

        flame.SetActive(false);
        smoke.SetActive(false);
        pointLight.enabled = false;
    }

    private void OnCandleInteracted(SelectEnterEventArgs args)
    {
        if (isLit) return;

        isLit = true;

        flame.SetActive(true);
        smoke.SetActive(true);
        pointLight.enabled = true;

        lightAudio.Play();

        puzzleManager.CandleLit(candleColor);
    }

    public void Extinguish()
    {
        isLit = false;
        flame.SetActive(false);
        smoke.SetActive(false);
        pointLight.enabled = false;
    }
}
