using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class DollController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovementDetector player;
    public float redLightDuration = 3f;
    public float greenLightDuration = 5f;
    public float animationTransitionTime = 1f;

    private bool isRedLight = false;
    private bool gameIsActive = true;
    private bool isFullyTurned = false;
    private bool gameIsOver = false;

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
                RestartScene();
                yield break;
            }
        }
    }

    void HandleGameOver()
    {
        gameIsActive = false;
        
    }

    void RestartScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        // HANDLE WIN HERE (PORTAL TO NEXT LEVEL OR SOMETHING)
    }
}