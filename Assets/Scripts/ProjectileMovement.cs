using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 10f;
    private float minY = -3.46f;
    private float maxY = 4.5f; // Límite vertical ajustado al tamaño del mapa
    private float minX = -10.78f;
    private float maxX = 10.7f;

    void Start()
    {
        // Incrementar el contador al instanciar el proyectil
        ProjectileManager.Instance.IncrementCount();
    }

    void Update()
    {
        // Mover el proyectil hacia adelante en la dirección de transform.up
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verificar si el proyectil ha salido del área visible de la cámara (vertical y horizontal)
        if (transform.position.y > maxY || transform.position.x > maxX || transform.position.x < minX || transform.position.y < minY)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        // Decrementar el contador cuando el proyectil es destruido
        if (ProjectileManager.Instance != null)
        {
            ProjectileManager.Instance.DecrementCount();
        }
    }
}
