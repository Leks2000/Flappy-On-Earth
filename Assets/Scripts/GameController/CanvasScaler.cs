using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    [Tooltip("Позиция объектов")]
    [SerializeField] private Vector3 baseScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private int pcWidth = 500;
    [SerializeField] private int pcHeight = 600;

    void Awake()
    {
        SetResolution();
        Adapt();
    }

    void Update()
    {
        if (IsResolutionChanged())
        {
            Adapt();
        }
    }

    void SetResolution()
    {
        if (IsPCPlatform())
        {
            Screen.SetResolution(pcWidth, pcHeight, true);
        }
    }

    bool IsPCPlatform()
    {
        return Application.platform == RuntimePlatform.WindowsPlayer ||
               Application.platform == RuntimePlatform.OSXPlayer ||
               Application.platform == RuntimePlatform.LinuxPlayer;
    }

    bool IsResolutionChanged()
    {
        return Screen.width != Screen.currentResolution.width || Screen.height != Screen.currentResolution.height;
    }

    void Adapt()
    {
        float scaleFactor = Screen.width / 1920f;
        Vector3 adaptedScale = new Vector3(baseScale.x * scaleFactor, baseScale.y * scaleFactor, baseScale.z);
        transform.localScale = adaptedScale;
    }
}
