using FirstGearGames.SmoothCameraShaker;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float damage;

    public ShakeData hitShakeData;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CameraShakerHandler.Shake(hitShakeData);
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            
            
            Destroy(gameObject);
        }
    }
}