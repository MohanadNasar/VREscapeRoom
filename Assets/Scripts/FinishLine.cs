using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public DollController dollController; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the finish line! You win!");
            dollController.PlayerWon();
        }
    }
}