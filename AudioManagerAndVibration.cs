using UnityEngine;

public class AudioManagerAndVibration : MonoBehaviour
{
    public static AudioManagerAndVibration Instance { get; private set; }

    
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip clickSound;
    public AudioClip winSound;

    
    public int baseIQ = 80;
    public int currentIQ = 80;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadIQ();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- SOUND EFFECTS ---
    public void PlayCorrectSound()
    {
        if (sfxSource && correctSound) sfxSource.PlayOneShot(correctSound);
        TriggerVibration(100); // Light haptic feedback
    }

    public void PlayWrongSound()
    {
        if (sfxSource && wrongSound) sfxSource.PlayOneShot(wrongSound);
        TriggerVibration(400); // Heavy error vibration
    }

    public void PlayButtonClick()
    {
        if (sfxSource && clickSound) sfxSource.PlayOneShot(clickSound);
    }

    // --- HAPTIC VIBRATION ---
    public void TriggerVibration(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            if (vibrator != null)
            {
                vibrator.Call("vibrate", milliseconds);
            }
        }
#endif
    }

    // --- IQ METER SYSTEM ---
    public void AddIQPoints(int points)
    {
        currentIQ += points;
        PlayerPrefs.SetInt("PlayerIQ", currentIQ);
        PlayerPrefs.Save();
        Debug.Log("🧠 Current IQ Score: " + currentIQ);
    }

    private void LoadIQ()
    {
        currentIQ = PlayerPrefs.GetInt("PlayerIQ", baseIQ);
    }
}