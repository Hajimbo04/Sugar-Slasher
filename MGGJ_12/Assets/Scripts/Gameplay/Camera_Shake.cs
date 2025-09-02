using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool useLocalSpace = true;

    Vector3 basePos;
    Coroutine running;
    float pendingTime;
    float pendingIntensity;

    void Awake()
    {
        if (!target) target = transform;
        basePos = useLocalSpace ? target.localPosition : target.position;
    }

    public void Shake(float intensity, float duration)
    {
        if (intensity <= 0f || duration <= 0f) return;
        pendingIntensity = Mathf.Max(pendingIntensity, intensity);
        pendingTime = Mathf.Max(pendingTime, duration);
        if (running == null) running = StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        var t = 0f;
        var dur = pendingTime;
        var amp = pendingIntensity;
        pendingTime = 0f;
        pendingIntensity = 0f;

        while (t < dur)
        {
            var falloff = 1f - (t / dur);
            var offset = Random.insideUnitSphere * amp * falloff;
            if (useLocalSpace) target.localPosition = basePos + offset;
            else target.position = basePos + offset;

            t += Time.deltaTime;

            if (pendingTime > 0f || pendingIntensity > 0f)
            {
                dur = Mathf.Max(dur - t, 0f) + pendingTime;
                amp = Mathf.Max(amp, pendingIntensity);
                t = 0f;
                pendingTime = 0f;
                pendingIntensity = 0f;
                basePos = useLocalSpace ? transform.localPosition : transform.position;
            }

            yield return null;
        }

        if (useLocalSpace) target.localPosition = basePos;
        else target.position = basePos;
        running = null;
    }
}
