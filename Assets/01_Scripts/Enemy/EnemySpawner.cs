using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float spawnMargin = 2f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int enemyPoolIndex = 2;

    private Camera mainCam;
    private float timer;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomSpawnPosition();

        GameObject enemy = GameManager.Instance.pool.Get(enemyPoolIndex);
        enemy.transform.position = spawnPos;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float height = mainCam.orthographicSize;
        float width = height * mainCam.aspect;

        Vector2 center = player.position;

        int side = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0: // 위
                x = Random.Range(-width, width);
                y = height + spawnMargin;
                break;

            case 1: // 아래
                x = Random.Range(-width, width);
                y = -height - spawnMargin;
                break;

            case 2: // 왼쪽
                x = -width - spawnMargin;
                y = Random.Range(-height, height);
                break;

            case 3: // 오른쪽
                x = width + spawnMargin;
                y = Random.Range(-height, height);
                break;
        }

        return center + new Vector2(x, y);
    }
}