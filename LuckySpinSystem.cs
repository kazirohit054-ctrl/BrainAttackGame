using UnityEngine;
using System;

public class LuckySpinSystem : MonoBehaviour
{
    [Header("Spin Settings")]
    public int[] rewardAmounts = { 20, 50, 100, 200, 500, 1000 };
    private const string LastSpinKey = "LastFreeSpinTime";

    // Daily Spin Check
    public bool CanSpinForFree()
    {
        if (!PlayerPrefs.HasKey(LastSpinKey)) return true;

        long lastSpinBinary = Convert.ToInt64(PlayerPrefs.GetString(LastSpinKey));
        DateTime lastSpinTime = DateTime.FromBinary(lastSpinBinary);
        
        // 24 ghante ka cooldown check
        return (DateTime.Now - lastSpinTime).TotalHours >= 24;
    }

    // Spin Action Logic
    public int SpinWheel(bool isAdRewarded = false)
    {
        if (!isAdRewarded && !CanSpinForFree())
        {
            Debug.Log("Free spin cooldown active! Watch an ad to spin.");
            return -1; // Cannot spin for free
        }

        // Random Reward Selection
        int randomIndex = UnityEngine.Random.Range(0, rewardAmounts.Length);
        int wonCoins = rewardAmounts[randomIndex];

        // Reward Add Karna
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoins(wonCoins);
        }

        // Free Spin Time Save Karna
        if (!isAdRewarded)
        {
            PlayerPrefs.SetString(LastSpinKey, DateTime.Now.ToBinary().ToString());
            PlayerPrefs.Save();
        }

        Debug.Log("Congratulations! You Won Coins: " + wonCoins);
        return wonCoins;
    }
}