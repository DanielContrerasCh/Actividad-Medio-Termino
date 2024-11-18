using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 10;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
