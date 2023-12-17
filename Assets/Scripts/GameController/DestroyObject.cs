using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!IsVisibleFromCamera() && GameObject.Find("Player").transform.position.y > transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    bool IsVisibleFromCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Collider2D objectCollider = GetComponent<Collider2D>();

        return GeometryUtility.TestPlanesAABB(planes, objectCollider.bounds);
    }
}
