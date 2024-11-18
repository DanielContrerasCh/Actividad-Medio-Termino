using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 10;
    private float minY = -3.46f;
    private float maxY = 4.5f; // Límite vertical ajustado al tamaño del mapa
    private float minX = -10.78f;
    private float maxX = 10.7f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        
        // Verificar si el proyectil ha salido del área visible de la cámara (vertical y horizontal)
        if (transform.position.y > maxY || transform.position.x > maxX || transform.position.x < minX || transform.position.y < minY)
        {
            Destroy(gameObject);
            return;
        }
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
