using UnityEngine;

public class ScrollingLandscape : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);
    }
}
