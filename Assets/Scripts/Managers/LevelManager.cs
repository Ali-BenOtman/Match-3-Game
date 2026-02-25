using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level Settings")]
    [SerializeField] private LevelData[] allLevels;

    [Header("UI Refrecnes")]
    [SerializeField] private Text levelText;

    private LevelData currentLevel;
    private int currentLevelIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (currentLevel == null)
        {
            LoadLevel(0); // This starts with the first level 
        }
    }
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < allLevels.Length)
        {
            currentLevelIndex = levelIndex;
            currentLevel = allLevels[levelIndex];

            Debug.Log("Loading " + currentLevel.levelName);

            if (levelText != null )
            {
                levelText.text = currentLevel.levelName;
            }

            //Apply level settings
            ApplyLevelSettings();
        }
    }

    void ApplyLevelSettings()
    {
        //Set target score
        MoveManager.Instance.SetTargetScore(currentLevel.targetScore);

        //Set moves
        MoveManager.Instance.SetMaxMoves(currentLevel.movesAllowed);

        //Update star thresholds
        GameManager.Instance.SetStarThreshold(
            currentLevel.oneStarScore,
            currentLevel.twoStarScore,
            currentLevel.threeStarScore
            );

        if (levelText != null)
        {
            
            levelText.text = currentLevel.levelName;
        }
    }

    public void LoadNextLevel()
    {
        int nextLevel = currentLevelIndex + 1;
        if (nextLevel < allLevels.Length)
        {
            LoadLevel(nextLevel);

            //Restart the game with new level
            //RESET GAME STATE
            GameManager.Instance.ResetGameState();
        }
        else
        {
            Debug.Log("No more levels! You won the game!!!");
            GameManager.Instance.ShowAllLevelsComplete();
            
        }
    }

    public LevelData GetCurrentLevel()
    {
        return currentLevel;
    }
    public int GetCurrentLevelNumber()
    {
        return currentLevel.levelNumber;
    }

     void OnEnable()
    {
        // Reconnects the UI refrences after scene reload
        if (levelText == null)
        {
            GameObject textObj = GameObject.Find("LevelText");
            if (textObj != null)
            {
                levelText = textObj.GetComponent<Text>();
            }
        }

        // Reapply level settings to update UI
        if (currentLevel !=  null)
        {
            ApplyLevelSettings();
        }
    }
}
