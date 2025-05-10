using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Camera mainCamera;
    public float boundaryPadding = .5f;
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool canDash = true;
    private bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(moveX, moveY, 0).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && canDash && moveDirection != Vector3.zero)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = moveDirection * moveSpeed;
        }

        ClampPositionToScreen();
    }

    void ClampPositionToScreen()
    {
        Vector3 pos = transform.position;

        float shipZ = pos.z;
        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, shipZ - mainCamera.transform.position.z));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, shipZ - mainCamera.transform.position.z));

        pos.x = Mathf.Clamp(pos.x, lowerLeft.x + boundaryPadding, upperRight.x - boundaryPadding);
        pos.y = Mathf.Clamp(pos.y, lowerLeft.y + boundaryPadding, upperRight.y - boundaryPadding);

        transform.position = pos;
    }


    System.Collections.IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = moveDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mineral"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
        }
    }
}
