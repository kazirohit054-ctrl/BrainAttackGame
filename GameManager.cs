using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalCoins = 100;
    public int currentLevel = 1;

    private void Awake()
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

    public void AddCoins(int amount)
    {
        totalCoins += amount;
    }

    public bool DeductCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            return true;
        }
        return false;
    }
}