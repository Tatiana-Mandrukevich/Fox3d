using UnityEngine;

public class ThirdPersonalController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSmoothTime = 0.12f;

    public Transform cameraTransform;

    private Rigidbody rb;
    private float turnSmoothVelocity;
    private Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg
                                + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                rotationSmoothTime
            );

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = moveDirection.normalized * moveSpeed;
        Vector3 newPosition = rb.position + velocity * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
    }
} 