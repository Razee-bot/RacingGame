using UnityEngine;

public class CarController : MonoBehaviour
{
    // ── Tunable values (adjust in Inspector) ──────────────────
    [Header("Movement Settings")]
    public float moveSpeed = 8f;       // base forward speed
    public float turnSpeed = 160f;     // rotation speed (degrees/sec)
    public float driftFactor = 0.95f;  // 1 = no drift, 0 = full drift

    // ── Runtime speed modifier (changed by boost/slow zones) ──
    [HideInInspector] public float speedMultiplier = 1f;

    // ── Private refs ──────────────────────────────────────────
    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    public bool canMove = true;   // set to false to freeze car

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove) return;

        // Read keyboard input
        moveInput = Input.GetAxis("Vertical");    // W/S or Up/Down
        turnInput = Input.GetAxis("Horizontal");  // A/D or Left/Right
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        // ── Get current speed in forward direction ─────────────────
        float currentSpeed = Vector2.Dot(rb.linearVelocity, transform.up);
        float maxSpeed = moveSpeed * speedMultiplier;

        // ── Acceleration & Braking ────────────────────────────────
        float acceleration = 0f;

        if (moveInput > 0) // Forward
        {
            // Accelerate gradually, but not beyond max speed
            if (currentSpeed < maxSpeed)
            {
                acceleration = moveInput * moveSpeed * 2f * Time.fixedDeltaTime;
            }
        }
        else if (moveInput < 0) // Reverse/Brake
        {
            if (currentSpeed > 0.5f)
            {
                // Braking: gradual slowdown, not instant stop
                acceleration = moveInput * moveSpeed * 1.5f * Time.fixedDeltaTime;
            }
            else
            {
                // Allow slow reverse when nearly stopped
                acceleration = moveInput * (moveSpeed * 0.5f) * Time.fixedDeltaTime;
            }
        }
        else
        {
            // No input: natural deceleration (friction)
            float deceleration = 1.5f * Time.fixedDeltaTime;
            if (currentSpeed > 0)
            {
                currentSpeed = Mathf.Max(0, currentSpeed - deceleration);
            }
            else if (currentSpeed < 0)
            {
                currentSpeed = Mathf.Min(0, currentSpeed + deceleration);
            }
        }

        // Apply acceleration to velocity
        Vector2 newVelocity = transform.up * (currentSpeed + acceleration);

        // ── Turning (only turn when moving) ────────────────────────
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnDir = -turnInput * turnSpeed * Time.fixedDeltaTime;
            // Flip turn direction when reversing
            if (currentSpeed < 0) turnDir = -turnDir;
            rb.rotation += turnDir;
        }

        // ── Reduce sideways drift (makes it feel like a car) ───────
        Vector2 forwardVel = transform.up * Vector2.Dot(newVelocity, transform.up);
        Vector2 rightVel   = transform.right * Vector2.Dot(newVelocity, transform.right);
        rb.linearVelocity = forwardVel + rightVel * driftFactor;
    }
}