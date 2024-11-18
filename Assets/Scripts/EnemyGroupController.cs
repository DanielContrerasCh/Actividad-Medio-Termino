using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public GameObject enemyProjectile;
    public float moveSpeed = 3f;
    public float fireRate = 1f;
    public float leftLimit = -9.98f;
    public float rightLimit = 9.88f;
    private float nextFireTime;
    private bool movingRight = true;
    private bool isMoving = false;
    private bool canShoot = true;

    void Update()
    {
        if (isMoving)
        {
            MoveGroup();

            // Disparar proyectiles hacia abajo si se permite
            if (canShoot && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopShooting()
    {
        canShoot = false;
    }

    void MoveGroup()
    {
        float leftmostEnemyX = transform.GetChild(0).position.x;
        float rightmostEnemyX = transform.GetChild(transform.childCount - 1).position.x;

        if (rightmostEnemyX >= rightLimit)
        {
            movingRight = false;
        }
        else if (leftmostEnemyX <= leftLimit)
        {
            movingRight = true;
        }

        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        Vector3 offset = new Vector3(-2, 4f, 0); // Ajuste vertical para disparar desde la posición correcta
        foreach (Transform enemy in transform)
        {
            Instantiate(enemyProjectile, enemy.position + offset, Quaternion.Euler(0, 0, 180));
        }
    }
}
