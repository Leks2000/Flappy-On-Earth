using System.Collections;
using UnityEngine;

public class MusicAnalyzer : MonoBehaviour
{
    private WaitForSeconds beatDelay = new WaitForSeconds(0.4f);
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private int spectrumSize;

    [SerializeField] private float beatThreshold;
    [SerializeField] private float sensitivity;

    [SerializeField] private float lowFrequencyThreshold;
    [SerializeField] private float highFrequencyThreshold;

    private float[] spectrumData;

    public delegate void DelayStateChanged(bool isDelaying);
    public static event DelayStateChanged OnDelayStateChanged;
    private bool isDelaying = false;

    void Start()
    {
        spectrumData = new float[spectrumSize];
        BeatDealay();
    }

    void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        AnalyzeSpectrum();
        if (!audioSource.isPlaying)
        {
            BeatDealay();
        }
    }
    private void BeatDealay()
    {
        int Index = PlayerPrefs.GetInt("IndexMusic");
        switch (Index)
        {
            case 0:
                {
                    beatThreshold = 0.04f;
                }
                break;
            case 1:
                {
                    beatThreshold = 0.005f;
                }
                break;
            case 2:
                {
                    beatThreshold = 0.05f;
                }
                break;
            case 3:
                {
                    beatThreshold = 0.005f;
                }
             break;
        }
    }
    void AnalyzeSpectrum()
    {
        float currentVolume = audioSource.volume;
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        if (CheckForBeat(currentVolume) && !isDelaying)
        {
            StartCoroutine(StartDelayAfterBeat());
        }
    }

    bool CheckForBeat(float currentVolume)
    {
        float adjustedBeatThreshold = beatThreshold * currentVolume;
        float lowFrequencyAmplitude = CalculateFrequencyAmplitude(0, lowFrequencyThreshold);
        float highFrequencyAmplitude = CalculateFrequencyAmplitude(lowFrequencyThreshold, highFrequencyThreshold);
        return lowFrequencyAmplitude > adjustedBeatThreshold && highFrequencyAmplitude > adjustedBeatThreshold;
    }

    float CalculateFrequencyAmplitude(float startFrequency, float endFrequency)
    {
        int startSample = (int)(startFrequency / AudioSettings.outputSampleRate * spectrumSize);
        int endSample = (int)(endFrequency / AudioSettings.outputSampleRate * spectrumSize);

        float amplitude = 0f;
        for (int i = startSample; i < endSample; i++)
        {
            amplitude += spectrumData[i];
        }
        amplitude /= (endSample - startSample);

        return amplitude;
    }

    IEnumerator StartDelayAfterBeat()
    {
        isDelaying = true;
        OnDelayStateChanged?.Invoke(isDelaying);
        yield return beatDelay;
        isDelaying = false;
        OnDelayStateChanged?.Invoke(isDelaying);
    }
}