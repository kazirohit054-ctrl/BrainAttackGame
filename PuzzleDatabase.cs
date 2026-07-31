using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PuzzleData
{
    public int levelNumber;
    public string questionText;
    public string hintText;
    public string solutionType; // e.g., "DRAG_OBJECT", "SHAKE_PHONE", "PINCH_SCALE"
}

public class PuzzleDatabase : MonoBehaviour
{
    public static PuzzleDatabase Instance { get; private set; }

    public List<PuzzleData> puzzleList = new List<PuzzleData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePuzzles();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePuzzles()
    {
        // --- LEVEL 1 ---
        puzzleList.Add(new PuzzleData
        {
            levelNumber = 1,
            questionText = "Which one is the biggest fruit?",
            hintText = "Don't look at the real-world size, look at the screen size!",
            solutionType = "CLICK_TARGET"
        });

        // --- LEVEL 2 ---
        puzzleList.Add(new PuzzleData
        {
            levelNumber = 2,
            questionText = "Help the elephant fit inside the small car!",
            hintText = "Pinch the elephant with two fingers to make it smaller.",
            solutionType = "PINCH_SCALE"
        });

        // --- LEVEL 3 ---
        puzzleList.Add(new PuzzleData
        {
            levelNumber = 3,
            questionText = "Wake up the sleeping cat!",
            hintText = "Shake your mobile phone to wake it up!",
            solutionType = "SHAKE_PHONE"
        });

        // --- LEVEL 4 ---
        puzzleList.Add(new PuzzleData
        {
            levelNumber = 4,
            questionText = "Put the sun behind the clouds to make it night.",
            hintText = "Drag the cloud object over the sun.",
            solutionType = "DRAG_OBJECT"
        });

        // --- LEVEL 5 ---
        puzzleList.Add(new PuzzleData
        {
            levelNumber = 5,
            questionText = "Find the hidden coin!",
            hintText = "Move the rock to see what is hidden behind it.",
            solutionType = "DRAG_OBJECT"
        });
    }

    public PuzzleData GetPuzzleForLevel(int level)
    {
        return puzzleList.Find(p => p.levelNumber == level);
    }
}