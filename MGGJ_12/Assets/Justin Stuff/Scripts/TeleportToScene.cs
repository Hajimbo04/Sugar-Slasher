using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public string sceneToTeleportTo = "GameWin";
	
	AudioManager audioManager;
	
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToTeleportTo);
			audioManager.PlayMusic(audioManager.bossEntry);
        }
    }
}