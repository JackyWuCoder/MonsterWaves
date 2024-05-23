using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;
    private string newGameScene = "Map_v1";

    void Start()
    {
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

    public void StartNewGame()
    {
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
