using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float normalSpeed = 8f;
    public float slowSpeed = 2f;
    public GameObject playerProjectile;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public int playerHealth = 100;
    public Vector3 firePointOffset = new Vector3(0, 1f, 0); // Ajusta el offset vertical

    private float nextFireTime;
    private bool isRotated = false;
    private Quaternion originalRotation;

    void Start()
    {
        // Guardar la rotación original del jugador
        originalRotation = transform.rotation;
    }

    void Update()
    {
        MovePlayer();

        // Disparar con la tecla Espacio
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        // Rotar el personaje con la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotatePlayer();
        }

        // Volver a la rotación normal al soltar la tecla R
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetRotation();
        }
    }

    void MovePlayer()
    {
        // Verificar si el jugador está en "modo lento"
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : normalSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Crear un vector de movimiento en coordenadas globales (no afectado por la rotación del jugador)
        Vector3 movement = new Vector3(moveX, moveY, 0);

        // Mover al jugador usando coordenadas globales
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

        // Actualizar la posición del firePoint con un offset
        firePoint.position = transform.position + firePointOffset;
    }


    void Shoot()
    {
        // Crear una rotación para que el proyectil apunte hacia la derecha
        Quaternion projectileRotation = Quaternion.Euler(0, 0, -90);
        Instantiate(playerProjectile, firePoint.position, projectileRotation);
    }

    void RotatePlayer()
    {
        // Rotar al jugador 90 grados para acostarlo
        transform.rotation = Quaternion.Euler(0, 180, 270);
        isRotated = true;
    }

    void ResetRotation()
    {
        // Restaurar la rotación normal del jugador
        transform.rotation = originalRotation;
        isRotated = false;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            // El jugador ha sido derrotado
            GameManager.Instance.EndGame("Game Over", gameObject);
        }
    }
}
