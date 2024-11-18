using UnityEngine;

public class EnemyColumnController : MonoBehaviour
{
    public GameObject enemyProjectile;
    public float moveSpeed = 2f;
    public float fireRate = 1f;
    public float upperLimit = 3.22f;
    public float lowerLimit = 1.36f;
    private float nextFireTime;
    private bool movingUp = true;
    private bool isMoving = false;
    private bool canShoot = true;

    void Update()
    {
        if (isMoving)
        {
            MoveColumn();

            // Disparar proyectiles hacia la derecha si se permite
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

    void MoveColumn()
    {
        if (transform.position.y >= upperLimit)
        {
            movingUp = false;
        }
        else if (transform.position.y <= lowerLimit)
        {
            movingUp = true;
        }

        float direction = movingUp ? 1f : -1f;
        transform.Translate(Vector3.up * direction * moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        Vector3 offset = new Vector3(-2, 4f, 0);
        foreach (Transform enemy in transform)
        {
            Instantiate(enemyProjectile, enemy.position + offset, Quaternion.Euler(0, 0, 270));
        }
    }
}
