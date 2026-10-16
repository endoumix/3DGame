using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Игрок не удаляется, а возвращается на старт
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.position = new Vector3(0, 2, 0);
                rb.velocity = Vector3.zero;          // <-- вот тут было linearVelocity
                rb.angularVelocity = Vector3.zero;
            }
            return;
        }

        // Всё остальное (роботов) — удаляем
        Destroy(other.gameObject);
    }
}
