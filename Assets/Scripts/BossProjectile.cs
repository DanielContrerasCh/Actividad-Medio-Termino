using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aplicar daño al jugador
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
