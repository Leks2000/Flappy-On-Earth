using UnityEngine;

public class CanvasScaler : MonoBehaviour
{
    [Tooltip("Позиция объектов")]
    [SerializeField] private Vector3 baseScale = new Vector3(1f, 1f, 1f);

    void Start()
    {
        Adapt();
    }

    void Update()
    {
        if (Screen.width != Screen.currentResolution.width || Screen.height != Screen.currentResolution.height)
        {
            Adapt();
        }
    }

    void Adapt()
    {
        float scaleFactor = Screen.width / 1920f;
        Vector3 adaptedScale = new Vector3(baseScale.x * scaleFactor, baseScale.y * scaleFactor, baseScale.z);
        transform.localScale = adaptedScale;
    }
}
