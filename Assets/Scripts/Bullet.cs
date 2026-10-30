using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 35f;

    [Header("„ь€ это пул€?")]
    public bool isEnemyBullet = false;

    void Start()
    {
        
        if (isEnemyBullet)
        {
            damage = 10f; 
        }

        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isEnemyBullet)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>(); // если попали в игрока с PlayerHealth Ч наносим урон
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
            return;
        }
        else
        {
            SimpleEnemy enemy = other.GetComponent<SimpleEnemy>(); // если попали во врага  Ч наносим урон и удал€ем пулю
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            return;
        }
    }
}