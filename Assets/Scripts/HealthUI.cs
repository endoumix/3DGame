using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthBar;
    public AudioSource hitSound;

    public void UpdateHealthBar(float current, float max)
    {
        if (healthBar != null)
        {
            healthBar.maxValue = max;
            healthBar.value = current;
        }
    }

    public void PlayHitSound()
    {
        if (hitSound != null)
        {
            hitSound.Stop();
            hitSound.Play();
        }
    }
}
