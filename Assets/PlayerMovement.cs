using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;          // скорость движения
    public float jumpForce = 5f;      // сила прыжка (можно не использовать)
    private Rigidbody rb;

    void Start()
    {
        // Берём компонент Rigidbody у объекта, к которому прикреплён скрипт
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Считываем нажатия клавиш
        float moveX = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо
        float moveZ = Input.GetAxis("Vertical");   // W/S или стрелки вверх/вниз

        // Направление движения в мировых координатах (по полу)
        Vector3 move = new Vector3(moveX, 0f, moveZ);

        // Двигаем шар: rb.position + смещение * скорость * время
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

        // Прыжок при нажатии пробела 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
