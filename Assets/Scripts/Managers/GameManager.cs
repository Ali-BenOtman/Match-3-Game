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
        nextLevelButton.onClick.AddListener(RestartGame);
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
        if (score >= 800) return 3;
        if (score >= 600) return 2;
        if (score >= 500) return 1;
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

    void RestartGame()
    {
        //Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
