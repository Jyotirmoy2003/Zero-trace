using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float speed = 20f;
    [SerializeField] float damage = 1f;
    [SerializeField] float lifeTime = 3f;


    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }
}
