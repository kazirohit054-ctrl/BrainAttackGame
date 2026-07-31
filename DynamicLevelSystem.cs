using UnityEngine;
using System.Collections.Generic;

public class DynamicLevelSystem : MonoBehaviour
{
    public static DynamicLevelSystem Instance { get; private set; }

    [Header("Level Progress Config")]
    public int totalMaxLevels = 1000;
    public int highestUnlockedLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Level Lock / Unlock Check
    public bool IsLevelUnlocked(int levelNumber)
    {
        return levelNumber <= highestUnlockedLevel;
    }

    // Player jab level jeet jaye
    public void OnLevelCompleted(int currentCompletedLevel)
    {
        if (currentCompletedLevel == highestUnlockedLevel && highestUnlockedLevel < totalMaxLevels)
        {
            highestUnlockedLevel++;
            SaveProgress();
            Debug.Log("🎉 New Level Unlocked: Level " + highestUnlockedLevel);
        }
    }

    // 1000+ Levels ke liye Auto Question & Target Generator
    public PuzzleData GetDynamicPuzzle(int levelNumber)
    {
        // Pehle se hand-crafted 5 Levels
        if (levelNumber <= 5 && PuzzleDatabase.Instance != null)
        {
            return PuzzleDatabase.Instance.GetPuzzleForLevel(levelNumber);
        }

        // Level 6 se 1000+ tak Procedural AI Logic
        PuzzleData dynamicPuzzle = new PuzzleData();
        dynamicPuzzle.levelNumber = levelNumber;

        // Difficulty Progression Logic (Harder as level increases)
        if (levelNumber % 3 == 0)
        {
            dynamicPuzzle.questionText = "Level " + levelNumber + ": Find the odd item hidden behind the objects!";
            dynamicPuzzle.hintText = "Try dragging the top items away!";
            dynamicPuzzle.solutionType = "DRAG_OBJECT";
        }
        else if (levelNumber % 3 == 1)
        {
            dynamicPuzzle.questionText = "Level " + levelNumber + ": Balance the scale to match 100%!";
            dynamicPuzzle.hintText = "Pinch and resize objects to match their weights.";
            dynamicPuzzle.solutionType = "PINCH_SCALE";
        }
        else
        {
            dynamicPuzzle.questionText = "Level " + levelNumber + ": Shake up the box to reveal the secret code!";
            dynamicPuzzle.hintText = "Physically shake your mobile device!";
            dynamicPuzzle.solutionType = "SHAKE_PHONE";
        }

        return dynamicPuzzle;
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("HighestUnlockedLevel", highestUnlockedLevel);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);
    }
}