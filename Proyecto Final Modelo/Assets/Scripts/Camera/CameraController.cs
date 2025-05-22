using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -6);
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float smoothTime = 0.1f;

    private Vector2 lookInput;
    private Vector2 rotation = Vector2.zero;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        Quaternion camTurnAngle = Quaternion.Euler(0, rotation.x, 0);
        Vector3 desiredPosition = target.position + camTurnAngle * offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(target);
    }

    private void RotateCamera(Vector2 value)
    {
        lookInput = value;
        rotation.x += lookInput.x * sensitivity;
        rotation.y -= lookInput.y * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -30f, 60f);
    }

    private void OnEnable()
    {
        InputReader.movementCamera += RotateCamera;
    }

    private void OnDisable()
    {
        InputReader.movementCamera -= RotateCamera;
    }
}
