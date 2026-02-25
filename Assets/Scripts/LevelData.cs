using UnityEngine;


[CreateAssetMenu(fileName =  "Level", menuName = "Match3/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    public int levelNumber;
    public string levelName;


    [Header("Level Settings")]
    public int targetScore;
    public int movesAllowed;

    [Header("Star Requirements")]
    public int oneStarScore;
    public int twoStarScore;
    public int threeStarScore;
}
