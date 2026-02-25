using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

    [Header("UI Refrences")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Button restartButton;

    [Header("Win UI References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Text starsText;
    [SerializeField] private Text winScoreText;
    [SerializeField] private Button nextLevelButton;

    private int oneStarThreshold = 500;
    private int twoStarThreshold = 700;
    private int threeStarThreshold = 900;


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
        // Hide game over panel at start
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        //Set up restart button 
        restartButton.onClick.AddListener(RestartGame);
        nextLevelButton.onClick.AddListener(LoadNextLevel);
    }
    public void ShowGameOver()
    {
        //Get Final score

        int finalScore = ScoreManager.Instance.GetScore();

        //Update UI
        finalScoreText.text = "Final Score: " + finalScore;

        //Show Panel
        gameOverPanel.SetActive(true);

        Debug.Log("Game OVer!! Final Score: " + finalScore);
    }

    public void ShowWinScreen()
    {
        Debug.Log("=== ShowWinScreen STARTED ===");

        int finalScore = ScoreManager.Instance.GetScore();
        Debug.Log("Got final score: " + finalScore);
        
        
        //Shows the threshhold for the scores
        Debug.Log("Star thresholds - 1★: " + oneStarThreshold + " | 2★: " + twoStarThreshold + " | 3★: " + threeStarThreshold);

        int stars = CalculateStars(finalScore);
        Debug.Log("Calculated stars: " + stars);

        // Update UI
        winScoreText.text = "Final Score: " + finalScore;
        Debug.Log("Updated win score text");

        starsText.text = GetStarString(stars);
        Debug.Log("Updated stars text");

        // Hide game over panel
        Debug.Log("Hiding game over panel...");
        gameOverPanel.SetActive(false);

        // Show win panel
        Debug.Log("Showing win panel...");
        winPanel.SetActive(true);

        Debug.Log("=== ShowWinScreen COMPLETED ===");
    }
    

    int CalculateStars(int score)
    {
        if (score >= threeStarThreshold) return 3;
        if (score >= twoStarThreshold) return 2;
        if (score >= oneStarThreshold) return 1;
        return 0;
    }

    string GetStarString(int stars)
    {
        switch (stars)
        {
            case 3: return "★★★";
            case 2: return "★★☆";
            case 1: return "★☆☆";
            default: return "☆☆☆";
        }
    }

    

    public void SetStarThreshold(int oneStar, int twoStar, int threeStar)
    {
        oneStarThreshold = oneStar;
        twoStarThreshold = twoStar;
        threeStarThreshold = threeStar;
    }


    //Full restart (reloads scene) - for the actual restart button
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //Resests game state without reloading scene - for next level
    public void ResetGameState()
    {
        //Hides all UI panels
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false );

        //Resests score
        ScoreManager.Instance.ResetScore();


       //Resets moves (will be set by level manager)
       MoveManager.Instance.ResetMoves();

        // Clear and regenerate board
        GameBoard board = FindFirstObjectByType<GameBoard>();
        if (board != null)
        {
            board.RegenerateBoard();
        }
        Debug.Log("Game state reset for next level");

    }

    void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }

    public void ShowAllLevelsComplete()
    {
        winScoreText.text = "ALL LEVELS COMPLETE -- Final Score: " + ScoreManager.Instance.GetScore();
        starsText.text = "YAAAY";
        winPanel.SetActive(true);

        // Hide next level button
        nextLevelButton.gameObject.SetActive(false);
    }

    
}
