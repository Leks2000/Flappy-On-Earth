using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private MovePlayer player;
    [SerializeField] private float xOffset;

    private void Update()
    {
        var position = transform.position;
        position.y = player.transform.position.y + xOffset;
        transform.position = position;
    }
}