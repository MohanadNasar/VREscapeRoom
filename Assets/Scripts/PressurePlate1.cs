using UnityEngine;

public class PressurePlate1 : MonoBehaviour
{
    public float requiredWeight = 10f;
    private float currentWeight = 0f;
    private bool isActivated = false;
    public int plateID = 2;
    public PlateManager manager;
    void OnTriggerEnter(Collider other)
    {
        grabableItems item = other.GetComponent<grabableItems>();
        if (item != null)
        {
            currentWeight += item.GetWeight();
            manager.UpdatePlateState(plateID, Mathf.Approximately(currentWeight, requiredWeight));
            Debug.Log($"Added weight: {item.GetWeight()} | Total: {currentWeight}");

        }

        //CheckActivation();
    }

    private void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<grabableItems>();
        if (item != null)
        {
            currentWeight -= item.GetWeight();
            manager.UpdatePlateState(plateID, Mathf.Approximately(currentWeight, requiredWeight));
        }
    }

    //void CheckActivation()
    //{
    //    if (Mathf.Approximately(currentWeight, requiredWeight) && !isActivated)
    //    {
    //        isActivated = true;
    //        Debug.Log("Plate1 activated!");
    //        // Add door opening or puzzle logic here
    //    }
    //    else if (currentWeight != requiredWeight && isActivated)
    //    {
    //        isActivated = false;
    //        Debug.Log("Plate deactivated!");
    //    }
    //}
}
