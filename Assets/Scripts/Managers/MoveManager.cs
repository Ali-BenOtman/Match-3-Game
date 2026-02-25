using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance;

    [Header("Move Settings")]
    [SerializeField] private int startingMoves = 30;

    [Header("UI References")]
    [SerializeField] private Text movesText;

    [Header("Win Settings")]
    [SerializeField] private int targetScore = 500;

    private int currentMoves;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentMoves = startingMoves;
        UpdateMovesUI();
    }

    // Use a move
    public void UseMove()
    {
        currentMoves--;
        UpdateMovesUI();

        Debug.Log("Moves remaining: " + currentMoves);

        // Check win/lose when moves run out
        if (currentMoves <= 0)
        {
            CheckWinCondition ();
        }


        
    }

    // Update the UI
    void UpdateMovesUI()
    {
        if (movesText != null)
        {
            movesText.text = "Moves: " + currentMoves;
        }
    }

    // Game over
    void GameOver()
    {
        Debug.Log("GAME OVER - No moves left!");
        //Wait a moment for cascades to finish, then show game over
        StartCoroutine(DelayedGameOver());
    }
    
System.Collections.IEnumerator DelayedGameOver()
    {
        Debug.Log("Game Over triggered - waiting for casscades....");

        BoardRefiller refiller = FindFirstObjectByType<BoardRefiller>();

        //Wait until all cascades finish
        while (refiller.IsProcessing())
        {
            Debug.Log("Still processing...waiting...");
            yield return null;
        }
        Debug.Log("Processing done!! Showing game over...");

        //Extra delay to ensure score is updated
        yield return new WaitForSeconds(1f);

        Debug.Log("Final Score: " + ScoreManager.Instance.GetScore());
        GameManager.Instance.ShowGameOver();
    }
        
        
        
   


    // Get current moves
    public int GetMoves()
    {
        return currentMoves;
    }

    // Reset moves
    public void ResetMoves()
    {
        currentMoves = startingMoves;
        UpdateMovesUI();
    }

    public void CheckWinCondition()
    {
        int currentScore = ScoreManager.Instance.GetScore();

        Debug.Log("CheckWinCondition called! Score: " + currentScore + " | Target: " + targetScore);
        //Only check win/ lose when moves run out 
        if (currentMoves <= 0)
        {
            //Check if they won (reached target score)
            if (currentScore >= targetScore)
            {
                Debug.Log("WIN CONDITION MET");
                GameWin();
            }
            else
            {
                Debug.Log("GAME OVER CONDITION MET");
                GameOver();
            }
        }
        
    }

    void GameWin()
    {
        Debug.Log("YOU WIN!!! Target score reached!!!");
        StartCoroutine(DelayedGameWin());
    }

    System.Collections.IEnumerator DelayedGameWin()
    {
        Debug.Log("Win Triggered - waiting for cascades.....");
        BoardRefiller refiller = FindFirstObjectByType<BoardRefiller>();

        //Waint until all cascades finish 
        while (refiller.IsProcessing())
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);

        Debug.Log("Final Score: " + ScoreManager.Instance.GetScore());
        GameManager.Instance.ShowWinScreen();
    }

    public void SetTargetScore(int score)
    {
        targetScore = score;
        Debug.Log("Target score set to: " +  targetScore);
    }

    public void SetMaxMoves(int moves)
    {
        startingMoves = moves;
        currentMoves = moves;
        UpdateMovesUI();
        Debug.Log("Moves set to: " + startingMoves);
    }
}