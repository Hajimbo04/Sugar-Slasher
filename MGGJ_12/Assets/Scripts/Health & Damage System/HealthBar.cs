using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    float targetValue;

    void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, targetValue, Time.deltaTime * 10f);
    }

    public void SetSlider(float amount)
    {
        targetValue = amount;
    }

    public void SetSliderMax(float amount)
    {
        healthSlider.maxValue = amount;
        targetValue = amount;
        healthSlider.value = amount;
    }
}
