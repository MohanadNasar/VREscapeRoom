using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class DollController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovementDetector player;
    public float redLightDuration = 3f;
    public float greenLightDuration = 4f;
    public float animationTransitionTime = 1f;

    private bool isRedLight = false;
    private bool gameIsActive = true;
    private bool isFullyTurned = false;
    private bool gameIsOver = false;

    public Transform playerTransform; 
    private Vector3 startPosition = new Vector3(-4f, 1.25f, -22.5f);

    public AudioSource gunshotAudio;

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (gameIsActive)
        {
            
            isRedLight = false;
            isFullyTurned = false;
            animator.SetBool("IsLooking", false);
            yield return new WaitForSeconds(greenLightDuration);

            
            isRedLight = true;
            animator.SetBool("IsLooking", true);

            
            yield return new WaitForSeconds(animationTransitionTime);
            isFullyTurned = true;

            
            float timer = redLightDuration;
            bool playerMoved = false;

            while (timer > 0 && !playerMoved && isFullyTurned)
            {
                if (player.isMoving)
                {
                    gunshotAudio.Play();
                    Debug.Log("Player moved during RED LIGHT! Game Over.");
                    playerMoved = true;
                    HandleGameOver();
                }

                timer -= Time.deltaTime;
                yield return null;
            }

            if (playerMoved)
            {
                
                yield return new WaitForSeconds(1f);
                ResetPlayerPosition();
                yield break;
            }
        }
    }

    void HandleGameOver()
    {
        gameIsActive = false;
        
    }

    void ResetPlayerPosition()
    {
        if (playerTransform != null)
        {
            playerTransform.position = startPosition;

            // Optional: Reset movement-related stuff (if using Rigidbody or CharacterController)
            Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Reset game loop
            gameIsActive = true;
            StartCoroutine(GameLoop());
        }
    }



    public void OnTurnComplete()
    {
        isFullyTurned = true;
    }

    public void PlayerWon()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            Debug.Log("Congratulations! You won!");
            animator.SetTrigger("Win");
            HandleWin();
        }
    }

    void HandleWin()
    {
        gameIsActive = false;
        
        StartCoroutine(WinSequence());
    }

    IEnumerator WinSequence()
    {
        
         animator.SetTrigger("Win");        
        yield return new WaitForSeconds(3f);
    }
}