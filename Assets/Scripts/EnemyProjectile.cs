using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f;
    
    void Update()
    {
        // Mover el proyectil hacia abajo
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
