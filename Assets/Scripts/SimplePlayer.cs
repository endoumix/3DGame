using UnityEngine;

public class SimplePlayer : MonoBehaviour
{
    [Header("Движение")]
    public float speed = 6f;
    public float mouseSensitivity = 2f;

    [Header("Стрельба")]
    public float fireRate = 0.3f; // задержка между выстрелами
    public Transform cameraTransform;
    public GameObject bulletPrefab;

    [Header("Звуки")]
    public AudioSource shootSound;
    public AudioSource stepSound;

    private Rigidbody rb;
    private float rotationX = 0f;
    private float nextFireTime = 0f;
    private float stepTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity; //повтороты мышкой 
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        rotationX -= mouseY;// крутим камеру вверх/вниз
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // 
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // задержка 
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 0) return;

        // Движение
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * moveX + transform.forward * moveZ).normalized;
        Vector3 velocity = direction * speed;// скорость движения
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        // если игрок реально движется 
        if (rb.velocity.magnitude > 0.5f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= 0.4f)// Каждые 0.4 сек новый шаг
            {
                if (stepSound != null)
                {
                    stepSound.pitch = Random.Range(0.9f, 1.1f); // шаги
                    stepSound.Play();
                }
                stepTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        // создаем пулю
        Vector3 spawnPos = cameraTransform.position + cameraTransform.forward * 1.5f;
        Instantiate(bulletPrefab, spawnPos, cameraTransform.rotation);

        
        if (shootSound != null)
        {
            shootSound.pitch = Random.Range(0.9f, 1.1f); 
            shootSound.Play();
        }
    }
}