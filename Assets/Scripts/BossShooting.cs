using System.Collections;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject projectileType1;
    public GameObject projectileType2;
    public GameObject projectileType3;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public float moveSpeed = 2f;
    public float minY, maxY;
    public Vector3 firePointOffset = new Vector3(0, 1f, 0);

    private enum ShootMode { Type1, Type2, Type3 }
    private ShootMode currentMode;
    private float nextFireTime;
    private bool isShooting = true;
    private Vector3 targetPosition;

    void Start()
    {
        // El jefe se inicia desactivado, no necesitamos iniciar la corrutina aquí
        gameObject.SetActive(false);
        currentMode = ShootMode.Type1;
        SetNewTargetPosition();
    }

    void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        MoveBoss();

        // Ajustar la posición del FirePoint con un offset
        firePoint.position = transform.position + firePointOffset;
    }

    public void ActivateBoss()
    {
        // Iniciar la corrutina para cambiar el modo de disparo cuando el jefe es activado
        StartCoroutine(ChangeShootingMode());
    }

    void Shoot()
    {
        switch (currentMode)
        {
            case ShootMode.Type1:
                FireType1();
                break;
            case ShootMode.Type2:
                FireType2();
                break;
            case ShootMode.Type3:
                FireType3();
                break;
        }
    }

    void FireType1()
    {
        Instantiate(projectileType1, firePoint.position, Quaternion.Euler(0, 0, 90));
    }

    void FireType2()
    {
        Instantiate(projectileType2, firePoint.position, Quaternion.Euler(0, 0, 75));
        Instantiate(projectileType2, firePoint.position, Quaternion.Euler(0, 0, 90));
        Instantiate(projectileType2, firePoint.position, Quaternion.Euler(0, 0, 105));
    }

    void FireType3()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(projectileType3, firePoint.position, Quaternion.Euler(0, 0, 70 + i * 5));
        }
    }

    IEnumerator ChangeShootingMode()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            currentMode = (ShootMode)(((int)currentMode + 1) % 3);
        }
    }

    void MoveBoss()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    void SetNewTargetPosition()
    {
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector3(transform.position.x, randomY, transform.position.z);
    }
}
