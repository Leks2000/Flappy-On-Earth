using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Слежение камеры")]
    [SerializeField] private PlayerController player;
    [SerializeField] private float xOffset;

    private void Update()
    {
        var position = transform.position;
        position.y = player.transform.position.y + xOffset;
        transform.position = position;
    }
}