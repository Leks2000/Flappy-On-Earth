using System.Collections;
using UnityEngine;

public class EffectShake : MonoBehaviour
{
    private Vector3 scale;
    [SerializeField] private float scaleMultiplier = 1.35f;
    [SerializeField] private float scaleDuration = 0.1f;

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
            int musicIndex = PlayerPrefs.GetInt("IndexMusic");

            switch (musicIndex)
            {
                case 0:
                    scaleMultiplier = 1.35f;
                    scaleDuration = 0.1f;
                    break;
                case 1:
                    scaleMultiplier = 1.4f;
                    scaleDuration = 0.03f;
                    break;
                case 2:
                    scaleMultiplier = 1.5f;
                    scaleDuration = 0.08f; 
                    break;
                case 3:
                    scaleDuration = 0.05f;
                    break;
            }

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
            transform.localScale = scale * scaleFactor;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        timer = 0f;
        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(scaleMultiplier, 1f, timer / scaleDuration);
            transform.localScale = scale * scaleFactor;
            yield return null;
        }
    }

    IEnumerator ScaleObject()
    {
        yield return new WaitForSeconds(0.1f);
        scale = transform.localScale;
        yield return null;
    }
}
