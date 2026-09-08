using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Camera mainCamera;
    private StarterAssets.StarterAssetsInputs input;

    void Awake()
    {
        mainCamera = Camera.main;
        input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    void Update()
    {
        if (input.shoot)
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Ray is hitting: " + hit.collider.gameObject.name);
            }
            input.ShootInput(false);
        }
    }
}