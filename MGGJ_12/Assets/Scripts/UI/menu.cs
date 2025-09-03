using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject menuObj, optionsObj, creditsObj, controlsObj;
    public string sceneName;
    public FadeManager fadeManager;
    
    AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        audioManager.PlayMusic(audioManager.mainMenuBg);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        
    }

    public void startGame()
    {
        audioManager.PlaySFX(audioManager.button);
        StartCoroutine(LoadSceneWithFade());
		audioManager.PlayMusic(audioManager.gameSceneBg);
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
        SceneManager.LoadScene(sceneName);
    }

    public void optionsMenu()
    {
        audioManager.PlaySFX(audioManager.button);
        menuObj.SetActive(false);
        optionsObj.SetActive(true);
    }

    public void creditsMenu()
    {
        audioManager.PlaySFX(audioManager.button);
        menuObj.SetActive(false);
        creditsObj.SetActive(true);
    }

    public void controlsMenu()
    {
        audioManager.PlaySFX(audioManager.button);
        menuObj.SetActive(false);
        controlsObj.SetActive(true);
    }

    public void quitGame()
    {
        audioManager.PlaySFX(audioManager.button);
        Debug.Log("This will quit the game, only works in actual build, not in Unity editor.");
        Application.Quit();
    }

    public void backToMenu()
    {
        audioManager.PlaySFX(audioManager.button);
        creditsObj.SetActive(false);
        optionsObj.SetActive(false);
        controlsObj.SetActive(false);
        menuObj.SetActive(true);
    }
}