using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonAnimator : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float animationSpeed = 15f;
    
    private Vector3[] originalScales;
    private Coroutine[] buttonCoroutines;
    
    void Start()
    {
        originalScales = new Vector3[buttons.Length];
        buttonCoroutines = new Coroutine[buttons.Length];
        
        for (int i = 0; i < buttons.Length; i++)
        {
            originalScales[i] = buttons[i].transform.localScale;
            AddEventTriggers(buttons[i], i);
        }
    }
    
    void AddEventTriggers(GameObject button, int index)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.AddComponent<EventTrigger>();
        
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((data) => { OnHover(index); });
        trigger.triggers.Add(hoverEntry);
        
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { OnExit(index); });
        trigger.triggers.Add(exitEntry);
    }
    
    void OnHover(int index)
    {
        if (buttonCoroutines[index] != null)
            StopCoroutine(buttonCoroutines[index]);
        buttonCoroutines[index] = StartCoroutine(ScaleButton(buttons[index], originalScales[index] * hoverScale));
    }
    
    void OnExit(int index)
    {
        if (buttonCoroutines[index] != null)
            StopCoroutine(buttonCoroutines[index]);
        buttonCoroutines[index] = StartCoroutine(ScaleButton(buttons[index], originalScales[index]));
    }
    
    IEnumerator ScaleButton(GameObject button, Vector3 targetScale)
    {
        Transform buttonTransform = button.transform;
        Vector3 currentScale = buttonTransform.localScale;
        
        while (Vector3.Distance(currentScale, targetScale) > 0.01f)
        {
            currentScale = Vector3.Lerp(currentScale, targetScale, animationSpeed * Time.deltaTime);
            buttonTransform.localScale = currentScale;
            yield return null;
        }
        
        buttonTransform.localScale = targetScale;
    }
}