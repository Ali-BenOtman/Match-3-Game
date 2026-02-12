using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    // Only one score manager exists
    public static ScoreManager Instance;

    [Header("Score Settings")]
    [SerializeField] private int pointsPerTile = 10;
    [SerializeField] private int comboMultiplier = 2;

    [Header("UI References")]
    [SerializeField] private Text scoreText;

    private int currentScore = 0;
    private int currentCombo = 0;

    void Awake()
    {
      //Singleton Setup
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
        UpdateScoreUI();
    }

    // Add points for matched tiles

    public void AddScore(int tilesMatched)
    {
        int points = tilesMatched * pointsPerTile;


        currentScore += points;

        UpdateScoreUI();

        Debug.Log("Score: " + currentScore + " (+" + points + " points)");

        //Increases combo for cascades  
        currentCombo++;
    }

    //Resset combo when player makes a new move
    public void ResetCombo()
    {
        if (currentCombo > 0)
        {
            Debug.Log("Combo ended at x" + currentCombo);
        }
        currentCombo = 0;
    }

    //Get current score 
    public int GetScore()
    {
        return currentScore;
    }

    // Reset score (for new game)
    public void ResetScore()
    {
       currentScore = 0;
        currentCombo = 0;
        UpdateScoreUI ();
        Debug.Log("Score reset");
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

}    

    

