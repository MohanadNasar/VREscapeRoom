using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public GameObject wallToHide;
    public BoxCollider boxColliderToHide;
    public AudioSource wallDisappearSound;

    private bool plate1Correct = false;
    private bool plate2Correct = false;
    private bool plate3Correct = false;
    private bool hasSoundPlayed = false;

    public void UpdatePlateState(int plateID, bool isCorrect)
    {
        switch (plateID)
        {
            case 1:
                plate1Correct = isCorrect;
                break;
            case 2:
                plate2Correct = isCorrect;
                break;
            case 3:
                plate3Correct = isCorrect;
                break;
        }

        CheckAllPlates();
    }

    private void CheckAllPlates()
    {
        if (plate1Correct && plate2Correct && plate3Correct)
        {
            if (!hasSoundPlayed)
            {
                wallDisappearSound.Play();
                hasSoundPlayed = true;
            }

            wallToHide.SetActive(false);
            boxColliderToHide.enabled = false;
        }
        else
        {
            wallToHide.SetActive(true);
        }
    }
}
