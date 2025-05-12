using UnityEngine;

public enum ObjectType { Mineral, Obstacle, SpeedBoost, Heal }

public class SpaceObject : MonoBehaviour
{
    [Header("Object Type")]
    public ObjectType type = ObjectType.Mineral;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float driftAmount = 0.5f;
    public float attractionStrength = 0.1f;

    [Header("Rotation Settings")]
    public float rotationSpeedMin = 20f;
    public float rotationSpeedMax = 80f;

    [Header("Mineral Settings")]
    public int pointValue = 1;

    private Vector3 rotationAxis;
    private float rotationSpeed;
    private Vector3 driftDirection;

    void Start()
    {
        rotationAxis = Random.onUnitSphere;
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
        driftDirection = new Vector3(
            Random.Range(-driftAmount, driftAmount),
            Random.Range(-driftAmount, driftAmount),
            0f
        );
    }

    void Update()
    {
        Move();
        Rotate();

        if (GameManager.Instance != null && transform.position.z < GameManager.Instance.Player.position.z - 10f)
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        if (type == ObjectType.Mineral)
        {
            KeepOnScreen();
        }
    }

    private void Move()
    {
        Vector3 moveDir = Vector3.back;

        if (type == ObjectType.Mineral && GameManager.Instance?.Player != null)
        {
            Vector3 toPlayer = (GameManager.Instance.Player.position - transform.position).normalized;
            moveDir = Vector3.back * (1f - attractionStrength) + toPlayer * attractionStrength;
            moveDir.Normalize();
        }

        float currentSpeed = GameManager.Instance != null && GameManager.Instance.IsSpeedBoostActive()
            ? moveSpeed * GameManager.Instance.speedMultiplier
            : moveSpeed;

        Vector3 velocity = (moveDir * currentSpeed + driftDirection) * Time.deltaTime;
        transform.Translate(velocity, Space.World);
    }

    private void Rotate()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    private void KeepOnScreen()
    {
        if (Camera.main == null) return;

        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        screenPos.x = Mathf.Clamp(screenPos.x, 0.05f, 0.95f);
        screenPos.y = Mathf.Clamp(screenPos.y, 0.05f, 0.95f);

        transform.position = Camera.main.ViewportToWorldPoint(screenPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var ship = other.GetComponent<ShipHealth>() ?? other.GetComponentInParent<ShipHealth>();

        switch (type)
        {
            case ObjectType.Mineral:
                GameManager.Instance?.AddScore(pointValue);
                break;

            case ObjectType.Obstacle:
                ship?.TakeDamage(1);
                break;

            case ObjectType.SpeedBoost:
                GameManager.Instance?.ActivateSpeedBoost();
                break;

            case ObjectType.Heal:
                ship?.Heal(1);
                break;
        }

        Destroy(gameObject);
    }
}
