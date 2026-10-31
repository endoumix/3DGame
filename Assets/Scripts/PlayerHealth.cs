using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    // ссылка на скрипт UI, чтобы сообщать ему об изменениях
    public HealthUI healthUI;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthUI != null) healthUI.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        // сообщаем UI, что здоровье изменилось
        if (healthUI != null) healthUI.UpdateHealthBar(currentHealth, maxHealth);
        if (healthUI != null) healthUI.PlayHitSound();

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (GameManager.Instance != null) GameManager.Instance.LoseGame();
    }
}