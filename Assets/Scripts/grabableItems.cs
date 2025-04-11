using UnityEngine;

public class grabableItems : MonoBehaviour
{
    [Header("Item Settings")]
    public float itemWeight = 5f; // Assign this in the Inspector

    public float GetWeight()
    {
        return itemWeight;
    }
}
