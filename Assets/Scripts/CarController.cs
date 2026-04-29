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

        // ── Forward / Backward movement ───────────────────────────
        float finalSpeed = moveSpeed * speedMultiplier;
        Vector2 moveForce = transform.up * moveInput * finalSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, moveForce, 0.1f);

        // ── Turning (only turn when moving) ───────────────────────
        float speed = rb.linearVelocity.magnitude;
        if (speed > 0.1f)
        {
            float turnDir = -turnInput * turnSpeed * Time.fixedDeltaTime;
            // Flip turn direction when reversing
            if (moveInput < 0) turnDir = -turnDir;
            rb.rotation += turnDir;
        }

        // ── Reduce sideways drift (makes it feel like a car) ──────
        Vector2 forwardVel = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 rightVel   = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);
        rb.linearVelocity = forwardVel + rightVel * driftFactor;
    }
}