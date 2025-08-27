using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject menuObj, optionsObj, creditsObj, controlsObj;
    public string sceneName;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene(sceneName);
    }
	public void optionsMenu()
    {
        menuObj.SetActive(false);
        optionsObj.SetActive(true);
    }
	public void creditsMenu()
    {
        menuObj.SetActive(false);
        creditsObj.SetActive(true);
    }
	public void controlsMenu()
    {
        menuObj.SetActive(false);
        controlsObj.SetActive(true);
    }
    public void quitGame()
    {
        Debug.Log("This will quit the game, only works in actual build, not in Unity editor.");
        Application.Quit();
    }
	public void backToMenu()
    {
        creditsObj.SetActive(false);
		optionsObj.SetActive(false);
		controlsObj.SetActive(false);
        menuObj.SetActive(true);
    }
}