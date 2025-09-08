using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    void Start()
    {
        // Get the name of the currently active scene.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if an AudioManager instance exists.
        if (AudioManager.instance != null)
        {
            // Use a switch statement to play the correct music based on the scene name.
            switch (currentSceneName)
            {
                case "Main_Menu_Scene":
                    AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuBg);
                    break;
                case "Game_Scene":
                    AudioManager.instance.PlayMusic(AudioManager.instance.gameSceneBg);
                    break;
                case "GameOver":
                    AudioManager.instance.PlayMusic(AudioManager.instance.gameOverBg);
                    break;
                // Add more cases for other scenes as needed.
                default:
                    Debug.LogWarning("No music defined for scene: " + currentSceneName);
                    break;
            }
        }
    }
}