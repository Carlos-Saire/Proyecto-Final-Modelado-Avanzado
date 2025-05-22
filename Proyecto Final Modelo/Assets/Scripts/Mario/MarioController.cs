using UnityEngine;

public class MarioController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Características")]
    [SerializeField] private float speed = 5f;

    [Header("Cámara")]
    [SerializeField] private Transform cameraTransform;

    private Vector2 inputPlayer;
    private Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * inputPlayer.y + right * inputPlayer.x;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }

    private void MovementPlayer(Vector2 value)
    {
        inputPlayer = value;
    }

    private void OnEnable()
    {
        InputReader.movementPlayer += MovementPlayer;
    }

    private void OnDisable()
    {
        InputReader.movementPlayer -= MovementPlayer;
    }
}
