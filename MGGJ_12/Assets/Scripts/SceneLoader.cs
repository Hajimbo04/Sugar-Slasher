using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public string sceneName1, sceneName2;
	public FadeManager fadeManager;
	private string sceneToLoad;
	
	AudioManager audioManager;
	
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}
	
	void Start()
	{
		
	}
	
    public void LoadGameScene()
    {
		audioManager.PlaySFX(audioManager.button);
		audioManager.PlayMusic(audioManager.gameSceneBg);
		sceneToLoad = sceneName1;
		StartCoroutine(LoadSceneWithFade());
    }
	
	public void LoadMainMenu()
	{
		audioManager.PlaySFX(audioManager.button);
		sceneToLoad = sceneName2;
		StartCoroutine(LoadSceneWithFade());
	}

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
		audioManager.PlayMusic(audioManager.gameOverBg);
    }
	
	IEnumerator LoadSceneWithFade()
    {
        if (fadeManager != null)
        {
            fadeManager.FadeIn();
            yield return new WaitForSeconds(1.3f);
        }
        else
        {
            yield return new WaitForSeconds(1.3f);
        }
        SceneManager.LoadScene(sceneToLoad);
    }

}
