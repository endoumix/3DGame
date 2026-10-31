using UnityEngine;
using UnityEngine.AI;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 8f;
    public int maxEnemies = 1;

    
    public Vector3[] spawnPoints;

    private float timer = 0f; 

    void Update()
    {
        if (Time.timeScale == 0) return;
        if (SimpleEnemy.enemiesKilled >= 5) return;

        timer += Time.deltaTime;
        if (timer >= spawnDelay)
        {
            timer = 0f;
            int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (currentEnemies < maxEnemies) TrySpawnEnemy();
        }
    }

    void TrySpawnEnemy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // ѕеремешиваем массив точек, чтобы выбрать случайную
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 point = spawnPoints[randomIndex];

        // ѕровер€ем, далеко ли эта точка от игрока
        if (Vector3.Distance(point, player.transform.position) > 12f)
        {
            Instantiate(enemyPrefab, point, Quaternion.identity);
        }
    }
}