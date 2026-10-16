using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("ƒвижение")]
    public float speed = 8f;
    public float acceleration = 30f;

    [Header(" амера")]
    public Transform cameraPivot;
    public float mouseSensitivity = 2.5f;
    public float maxPitch = 80f;

    private Rigidbody rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private float pitch = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();

        // WASD Ч нова€ Input System
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // ѕрыжок отключЄн Ч игра про таран, а не про прыжки
        // controls.Player.Jump.performed += _ => TryJump();
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // ћышь Ч через legacy API (в твоей версии Input System глючит с Mouse Delta)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up, mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);
        if (cameraPivot != null)
            cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void FixedUpdate()
    {
        Vector3 dir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        Vector3 targetVel = dir * speed;

        Vector3 currentVel = rb.velocity;   // Unity 2022 Ч velocity
        Vector3 newVel = Vector3.MoveTowards(currentVel, targetVel, acceleration * Time.fixedDeltaTime);
        newVel.y = currentVel.y;            // вертикаль ведЄт физика
        rb.velocity = newVel;
    }
}