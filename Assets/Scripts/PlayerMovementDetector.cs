using UnityEngine;

public class PlayerMovementDetector : MonoBehaviour
{
    public bool isMoving = false;
    private Vector3 lastPosition;
    public float movementThreshold = 0.001f; 

    void Start()
    {
        lastPosition = transform.position;
        Debug.Log(lastPosition);
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        isMoving = distanceMoved > movementThreshold;
        lastPosition = transform.position;
    }
}
