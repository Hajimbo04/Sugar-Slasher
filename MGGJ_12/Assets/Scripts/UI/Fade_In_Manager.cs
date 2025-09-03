using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private float fadeDuration = 1f;
    private CanvasGroup canvasGroup;
    
    void Start()
    {
        if (fadePanel != null)
        {
            canvasGroup = fadePanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = fadePanel.AddComponent<CanvasGroup>();
            
            canvasGroup.alpha = 0f;
            fadePanel.SetActive(false);
        }
    }
    
    public void FadeIn()
    {
        if (fadePanel != null)
        {
            fadePanel.SetActive(true);
            StartCoroutine(Fade(0f, 1f));
        }
    }
    
    public void FadeOut()
    {
        if (fadePanel != null)
            StartCoroutine(FadeOutCoroutine());
    }
    
    public void FadeIn(float duration)
    {
        if (fadePanel != null)
        {
            fadePanel.SetActive(true);
            StartCoroutine(Fade(0f, 1f, duration));
        }
    }
    
    public void FadeOut(float duration)
    {
        if (fadePanel != null)
            StartCoroutine(FadeOutCoroutine(duration));
    }
    
    private IEnumerator FadeOutCoroutine(float duration = -1f)
    {
        yield return StartCoroutine(Fade(1f, 0f, duration));
        if (fadePanel != null)
            fadePanel.SetActive(false);
    }
    
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration = -1f)
    {
        if (duration == -1f)
            duration = fadeDuration;
            
        float elapsedTime = 0f;
        canvasGroup.alpha = startAlpha;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }
        
        canvasGroup.alpha = endAlpha;
    }
}