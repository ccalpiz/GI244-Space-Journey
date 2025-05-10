using UnityEngine;

public enum ObjectType { Mineral, Obstacle }

public class SpaceObject : MonoBehaviour
{
    public ObjectType type = ObjectType.Mineral;

    [Header("General Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeedMin = 20f;
    public float rotationSpeedMax = 80f;
    public float driftAmount = 0.5f;
    public int pointValue = 1;
    public float attractionStrength = 0.1f;

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
        Vector3 moveDir;

        if (type == ObjectType.Mineral && GameManager.Instance.Player != null)
        {
            Vector3 toPlayer = (GameManager.Instance.Player.position - transform.position).normalized;
            moveDir = (Vector3.back * (1f - attractionStrength) + toPlayer * attractionStrength).normalized;
        }
        else
        {
            moveDir = Vector3.back;
        }

        transform.Translate((moveDir * moveSpeed + driftDirection) * Time.deltaTime, Space.World);
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

        if (transform.position.z < GameManager.Instance.Player.position.z - 10f)
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        if (type == ObjectType.Mineral)
            KeepOnScreen();
    }

    void KeepOnScreen()
    {
        if (Camera.main == null) return;

        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        screenPos.x = Mathf.Clamp(screenPos.x, 0.05f, 0.95f);
        screenPos.y = Mathf.Clamp(screenPos.y, 0.05f, 0.95f);
        transform.position = Camera.main.ViewportToWorldPoint(screenPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == ObjectType.Mineral)
            {
                GameManager.Instance.AddScore(pointValue);
                Destroy(gameObject);
            }
            else if (type == ObjectType.Obstacle)
            {
                ShipHealth ship = other.GetComponent<ShipHealth>();
                if (ship == null)
                    ship = other.GetComponentInParent<ShipHealth>();

                if (ship != null)
                {
                    ship.TakeDamage(1);
                    Destroy(gameObject);
                }
            }
        }
    }
}
