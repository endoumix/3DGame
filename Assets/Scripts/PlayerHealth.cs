using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Здоровье")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthBar;

    [Header("Звук")]
    public AudioSource hitSound; 

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBar != null) healthBar.value = currentHealth;

        //звук попадания
        if (hitSound != null)
        {
            // чтобы не было каши делаем там что если попали а потом снова попали чтоб звук попадания начался заново ,чтобы не было каши
            hitSound.Stop();
            hitSound.Play();
        }

        // Если здоровье кончилось — умираем
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die() //  сообщаем GameManager о поражении игрока
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseGame();
        }
    }
}