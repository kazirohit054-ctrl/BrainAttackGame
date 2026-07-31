using UnityEngine;

public class AIHintManager : MonoBehaviour
{
    public static AIHintManager Instance { get; private set; }

    [Header("AI Sensitivity Config")]
    public int maxAllowedWrongAttempts = 3;
    public float maxStuckTimeSeconds = 30f;

    private int currentWrongAttempts = 0;
    private float timerOnCurrentLevel = 0f;
    private bool isLevelActive = false;

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

    private void Update()
    {
        if (isLevelActive)
        {
            timerOnCurrentLevel += Time.deltaTime;

            // Automation Check: Player agar 30 sec se ziada atka hua hai
            if (timerOnCurrentLevel >= maxStuckTimeSeconds)
            {
                TriggerAIAutoHint("Player seems stuck for 30s. Triggering Smart Hint!");
                timerOnCurrentLevel = 0f; // Reset timer
            }
        }
    }

    public void StartLevelTracking()
    {
        currentWrongAttempts = 0;
        timerOnCurrentLevel = 0f;
        isLevelActive = true;
    }

    public void RegisterWrongAttempt()
    {
        currentWrongAttempts++;
        Debug.Log("Wrong Attempt Registered: " + currentWrongAttempts);

        // Automation Check: Player agar 3 baar galat try kar chuka hai
        if (currentWrongAttempts >= maxAllowedWrongAttempts)
        {
            TriggerAIAutoHint("3 Wrong Attempts Detected. Offering AI Guidance!");
            currentWrongAttempts = 0; // Reset counter
        }
    }

    public void CompleteLevelSuccess()
    {
        isLevelActive = false;
        currentWrongAttempts = 0;
        timerOnCurrentLevel = 0f;
    }

    private void TriggerAIAutoHint(string reason)
    {
        Debug.Log("🤖 [AI AUTOMATION]: " + reason);
        // Yahan UI par AI Hint Popup enable ho jayega
        // UIManager.Instance.ShowAIHintPopup();
    }
}