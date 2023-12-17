using System.Collections;
using UnityEngine;

public class EffectShake : MonoBehaviour
{
    private Vector3 Scale;
    public float scaleMultiplier = 1.135f;
    public float scaleDuration = 0.1f;

    private void OnEnable()
    {
        MusicAnalyzer.OnDelayStateChanged += HandleDelayStateChanged;
    }

    private void OnDisable()
    {
        MusicAnalyzer.OnDelayStateChanged -= HandleDelayStateChanged;
    }

    private void HandleDelayStateChanged(bool isDelaying)
    {
        if (isDelaying)
        {
            StartCoroutine(ScalePlayer());
        }
    }

    void Start()
    {
        StartCoroutine(ScaleObject());
    }

    IEnumerator ScalePlayer()
    {
        float timer = 0f;
        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, scaleMultiplier, timer / scaleDuration);
            transform.localScale = Scale * scaleFactor;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        timer = 0f;
        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(scaleMultiplier, 1f, timer / scaleDuration);
            transform.localScale = Scale * scaleFactor;
            yield return null;
        }
    }

    IEnumerator ScaleObject()
    {
        yield return new WaitForSeconds(0.1f);
        Scale = transform.localScale;
        yield return null;
    }
}
