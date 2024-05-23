using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;
    private string newGameScene = "Map_v1";

    public AudioSource mainChannel;
    public AudioClip backgroundMusic;

    public AudioSource playChannel;
    public AudioClip onPlayClip;
    public float onPlayDelay = 5.0f; // Delay in seconds

    void Start()
    {
        mainChannel.clip = backgroundMusic;
        mainChannel.loop = true;
        mainChannel.Play();
        // Start the coroutine to play the audio with looping and delay
        StartCoroutine(LoopAudioWithDelay());
        // Ensure SaveLoadManager.Instance is not null before calling LoadHighScore
        if (SaveLoadManager.Instance != null)
        {
            // Set the high score text
            int highScore = SaveLoadManager.Instance.LoadHighScore();
            highScoreUI.text = $"Highest Waves Survived: {highScore}";
        }
        else
        {
            Debug.LogError("SaveLoadManager.Instance is null. Make sure SaveLoadManager is initialized correctly.");
        }
    }

    IEnumerator LoopAudioWithDelay()
    {
        while (true)
        {
            // Set the audio clip and enable looping
            playChannel.clip = onPlayClip;
            playChannel.loop = false;

            // Play the audio clip
            playChannel.Play();

            // Wait for the audio clip to finish
            yield return new WaitForSeconds(playChannel.clip.length);

            // Wait for the specified delay between loops
            yield return new WaitForSeconds(onPlayDelay);
        }
    }

    public void StartNewGame()
    {
        mainChannel.Stop();
        playChannel.Stop();
        SceneManager.LoadScene(newGameScene);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
