using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
	[SerializeField] AudioSource SFXSource;
	
	[Header("---------Audio Clip---------")]
	public AudioClip mainMenuBg;
	public AudioClip button;
	public AudioClip gameOverBg;
	
	private void Start()
	{
		//musicSource.clip = mainMenuBg;
		//musicSource.Play();
	}
	
	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play();
	}
	
	public void PlaySFX(AudioClip clip)
	{
		SFXSource.PlayOneShot(clip);
	}
}
