using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float initialSpawnInterval = 1.5f;
    public float minSpawnInterval = 0.3f;
    public float difficultyIncreaseRate = 0.1f;
    private float spawnInterval;
    private float spawnTimer;
    private float timeElapsed;

    [Header("Difficulty Settings")]
    public float difficultyStepTime = 5f;

    [Header("Spawn Area")]
    public Transform topSpawn;
    public Transform bottomSpawn;
    public Transform leftSpawn;
    public Transform rightSpawn;

    [Header("Enemy Settings")]
    public float minDistance = 1.5f;

    [Header("Control")]
    public bool canSpawn = false; // ?? m?c ??nh false

    void Start()
    {
        spawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        if (!canSpawn) return; // ?? n?u ch?a b?m Play thì không spawn gì c?

        timeElapsed += Time.deltaTime;

        // t?ng ?? khó sau m?i X giây
        if (timeElapsed >= difficultyStepTime)
        {
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - difficultyIncreaseRate);
            timeElapsed = 0f;
        }

        // spawn enemy
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int maxAttempts = 10; // th? t?i ?a 10 l?n
        int attempts = 0;
        bool spawned = false;

        while (!spawned && attempts < maxAttempts)
        {
            attempts++;

            int side = Random.Range(0, 4);
            Vector3 pos = Vector3.zero;

            switch (side)
            {
                case 0: pos = topSpawn.position; break;
                case 1: pos = bottomSpawn.position; break;
                case 2: pos = leftSpawn.position; break;
                case 3: pos = rightSpawn.position; break;
            }

            Collider2D hit = Physics2D.OverlapCircle(pos, minDistance, LayerMask.GetMask("Enemy"));
            if (hit == null)
            {
                Instantiate(enemyPrefab, pos, Quaternion.identity);
                spawned = true; // spawn thành công ? thoát vòng l?p
            }
        }
    }
}
