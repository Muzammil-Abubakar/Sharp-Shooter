using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    public GameObject robotPrefab;

    private float spawnTimer;

    private void Update()
    {
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        PlayerHealth player = FindAnyObjectByType<PlayerHealth>();

        if (player == null)
        {
            spawnTimer = 0f;
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 5f)
        {
            Vector3 spawnPosition = transform.position + transform.forward * 2f;

            GameObject robotObject = Instantiate(
                robotPrefab,
                spawnPosition,
                transform.rotation
            );

            Robot robot = robotObject.GetComponent<Robot>();
            robot.SetTarget(player.transform);

            spawnTimer = 0f;
        }
    }
}