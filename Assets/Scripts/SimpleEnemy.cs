using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    [Header("Стрельба")]
    public float fireRate = 1.5f;
    public GameObject bulletPrefab;

    [Header("Звук")]
    public AudioSource shootSound;

    public float maxHealth = 100f;
    private float currentHealth; //текущее здоровье врага
    private Transform player;
    private float nextFireTime = 0f; //задержка между выстрелами
    private NavMeshAgent agent;

    public static int enemiesKilled = 0;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>(); // говорим врагу через агент "иди к игроку" 
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null || Time.timeScale == 0) return;

        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 12f && Time.time >= nextFireTime)
        {
            RaycastHit hit;
            Vector3 dir = (player.position - transform.position).normalized;// вычисляем направление от врага к игроку
            if (Physics.Raycast(transform.position, dir, out hit, 15f))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    nextFireTime = Time.time + fireRate;
                    Shoot();
                }
            }
        }
    }

    void Shoot()
    {
        Vector3 spawnPos = transform.position + Vector3.up * 0.5f; //пулю выше ставлю
        Vector3 direction = (player.position - spawnPos).normalized;// направление от врага к игроку
        Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(direction));

        
        if (shootSound != null)
        {
            shootSound.pitch = Random.Range(0.9f, 1.1f); 
            shootSound.Play();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;//отняли здоровье 
        if (currentHealth <= 0)
        {
            enemiesKilled++;
            if (enemiesKilled >= 5 && GameManager.Instance != null)
            {
                GameManager.Instance.WinGame();
            }
            Destroy(gameObject);
        }
    }
}