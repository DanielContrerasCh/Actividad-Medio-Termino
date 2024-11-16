using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        // Incrementar el contador al instanciar el proyectil
        ProjectileManager.Instance.IncrementCount();

        // Destruir el proyectil después de un tiempo para evitar acumulación
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mover el proyectil hacia adelante (eje Z)
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        // Decrementar el contador cuando el proyectil es destruido
        if (ProjectileManager.Instance != null)
        {
            ProjectileManager.Instance.DecrementCount();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Aquí puedes añadir lógica para manejar colisiones
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
