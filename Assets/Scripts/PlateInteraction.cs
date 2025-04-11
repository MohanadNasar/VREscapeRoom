using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlateInteraction : MonoBehaviour
{

    public GameObject panel;



    private bool isAppear = false;

    //public PlatePuzzleManager puzzleManager;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnPlateInteracted);

        panel.SetActive(false);


    }

    private void OnPlateInteracted(SelectEnterEventArgs args)
    {
        TogglePanel(); // Toggle the panel visibility on each interaction
    }


    private void TogglePanel()
    {
        // Toggle the panel's active state
        isAppear = !isAppear;
        panel.SetActive(isAppear);
    }
    //public void Extinguish()
    //{

    //    panel.SetActive(false);

    //}
}