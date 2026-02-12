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

        //Set up restart button 
        restartButton.onClick.AddListener(RestartGame);
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

    void RestartGame()
    {
        //Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
