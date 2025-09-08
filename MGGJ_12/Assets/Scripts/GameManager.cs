using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject audioManagerPrefab;

    void Awake()
    {
        if (AudioManager.instance == null)
        {
            Instantiate(audioManagerPrefab);
        }
    }
}