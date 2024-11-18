using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 5f;
    private float minY = -3.46f;
    private float maxY = 5; // Límite vertical ajustado al tamaño del mapa
    private float horizontalLimit = 10f; // Límite horizontal ajustado al tamaño del mapa
    private float minX = -10.78f;
    private float maxX = 10.7;

    void Start()
    {
        // Incrementar el contador al instanciar el proyectil
        ProjectileManager.Instance.IncrementCount();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Mover el proyectil hacia adelante en la dirección de transform.up
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verificar si el proyectil ha salido del área visible de la cámara (vertical y horizontal)
        if (Mathf.Abs(transform.position.y) > verticalLimit || Mathf.Abs(transform.position.x) > horizontalLimit)
        {
            Destroy(gameObject);
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
