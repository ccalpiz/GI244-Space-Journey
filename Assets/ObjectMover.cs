using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyZ = -10f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
