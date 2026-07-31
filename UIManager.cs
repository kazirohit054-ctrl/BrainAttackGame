using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    public GameObject homeScreen;
    public GameObject levelSelectScreen;
    public GameObject gamePlayScreen;
    public GameObject shopIAPScreen;
    public GameObject aiHintPopup;
    public GameObject luckySpinScreen;

    
    public Text coinText;
    public Text levelTitleText;

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

    private void Start()
    {
        ShowHomeScreen();
        UpdateCoinUI(PlayerPrefs.GetInt("PlayerCoins", 100));
    }

    // --- SCREEN NAVIGATION LOGIC ---

    public void ShowHomeScreen()
    {
        HideAllScreens();
        homeScreen.SetActive(true);
    }

    public void ShowLevelSelectScreen()
    {
        HideAllScreens();
        levelSelectScreen.SetActive(true);
    }

    public void ShowGameplayScreen(int levelIndex)
    {
        HideAllScreens();
        gamePlayScreen.SetActive(true);
        if (levelTitleText != null) 
            levelTitleText.text = "LEVEL " + levelIndex;
        
        AIHintManager.Instance?.StartLevelTracking();
    }

    public void ShowShopScreen()
    {
        HideAllScreens();
        shopIAPScreen.SetActive(true);
    }

    public void ShowLuckySpinScreen()
    {
        luckySpinScreen.SetActive(true);
    }

    public void ShowAIHintPopup()
    {
        aiHintPopup.SetActive(true);
    }

    public void CloseAIHintPopup()
    {
        aiHintPopup.SetActive(false);
    }

    private void HideAllScreens()
    {
        homeScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        shopIAPScreen.SetActive(false);
        luckySpinScreen.SetActive(false);
        aiHintPopup.SetActive(false);
    }

    public void UpdateCoinUI(int coins)
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }
    }
}