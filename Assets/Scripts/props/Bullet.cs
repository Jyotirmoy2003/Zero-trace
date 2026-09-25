using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float speed = 20f;
    private float damage;
    private float lifeTime = 3f;
    public Bullet(float damage,float lifeTime)
    {
        this.damage = damage;
        this.lifeTime = lifeTime;
    }


    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
    }
}
