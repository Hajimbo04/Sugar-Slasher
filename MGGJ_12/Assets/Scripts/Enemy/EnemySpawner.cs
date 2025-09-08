using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject playerObject;


    public GameObject[] enemiesToSpawn;


    void Start()
    {

        foreach (GameObject enemy in enemiesToSpawn)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            foreach (GameObject enemy in enemiesToSpawn)
            {
                if (enemy != null)
                {
                    enemy.SetActive(true);
                }
            }

            this.enabled = false;

            // Optional: You could also destroy the cube itself if you want it to disappear.
            // Destroy(gameObject);
        }
    }
}