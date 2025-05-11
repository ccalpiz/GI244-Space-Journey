using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float boundaryPadding = 0.5f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("References")]
    public Camera mainCamera;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool canDash = true;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 🛠️ Auto-assign if missing
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    void Update()
    {
        HandleMovementInput();
        HandleDashInput();
        Debug.Log("Move direction: " + moveDirection);
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }

        Vector3 v = rb.linearVelocity;
        v.z = 0f;
        rb.linearVelocity = v;

        ClampPositionToScreen();
    }

    void HandleMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(moveX, moveY, 0f).normalized;
    }

    void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash && moveDirection != Vector3.zero)
        {
            StartCoroutine(Dash());
        }
    }

    System.Collections.IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = moveDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void ClampPositionToScreen()
    {
        Vector3 pos = transform.position;
        float shipZ = pos.z;

        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, shipZ - mainCamera.transform.position.z));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, shipZ - mainCamera.transform.position.z));

        pos.x = Mathf.Clamp(pos.x, lowerLeft.x + boundaryPadding, upperRight.x - boundaryPadding);
        pos.y = Mathf.Clamp(pos.y, lowerLeft.y + boundaryPadding, upperRight.y - boundaryPadding);
        pos.z = 0f;

        transform.position = pos;
    }
}
