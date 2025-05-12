using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [Header("Mover Settings")]
    public float speed = 5f;
    public float destroyZ = -10f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
