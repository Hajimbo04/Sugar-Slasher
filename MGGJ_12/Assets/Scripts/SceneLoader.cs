using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	AudioManager audioManager;
	
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}
	
	void Start()
	{
		audioManager.PlayMusic(audioManager.gameOverBg);
	}
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadGameScene()
    {
		audioManager.PlaySFX(audioManager.button);
        SceneManager.LoadScene("Joyce_Scene");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
