using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Create a ray from the center of the main camera
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // Fire the ray
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Ray is hitting: " + hit.collider.gameObject.name);
        }
    }
}