using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Camera")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerHead;
    private float verticalRotStore;
    private Vector2 mouseInput;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityMode;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canJump;
    private Vector3 moveDirection;
    private Vector3 movement;

    private CharacterController controller;
    private Camera mainCameraRef;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCameraRef = Camera.main;

        CursorControl.LockCursor();
    }

    private void Update()
    {
        PlayerCamera();

        PlayerMovement();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.25f, groundLayer);
    }

    private void LateUpdate()
    {
        mainCameraRef.transform.SetPositionAndRotation(playerHead.position, playerHead.rotation);
    }

    private void PlayerCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        mouseInput = new Vector2(mouseX, mouseY) * mouseSensitivity;

        Vector3 localEulerAngles = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(localEulerAngles.x, localEulerAngles.y + mouseInput.x, localEulerAngles.z);

        Vector3 headRotation = playerHead.rotation.eulerAngles;

        playerHead.rotation = Quaternion.Euler(CameraVerticalRotation(), headRotation.y, headRotation.z);
    }

    private float CameraVerticalRotation()
    {
        verticalRotStore += mouseInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f);
        return -verticalRotStore;
    }

    private void PlayerMovement()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        float Yvelocity = movement.y;
        movement = ((transform.forward * moveDirection.z) + (transform.right * moveDirection.x)).normalized;
        movement.y = Yvelocity;

        if (controller.isGrounded) { movement.y = 0f; }

        PlayerJump();

        movement.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(moveSpeed * Time.deltaTime * movement);
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            movement.y = jumpForce * gravityMode;
        }
    }
}
