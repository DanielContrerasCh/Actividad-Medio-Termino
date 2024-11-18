using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject enemyGroupPrefab;
    public GameObject enemyColumnPrefab;
    public GameObject boss;
    public TextMeshProUGUI enemyCounterText;
    public float waveDuration = 10f;
    public float startX;
    public float moveSpeed = 3f;
    private int activeEnemies = 0;

    void Start()
    {
        StartCoroutine(HandleWaves());
    }

    IEnumerator HandleWaves()
    {
        // Primera oleada: grupo de enemigos apareciendo desde la derecha
        Vector3 spawnPosition = new Vector3(startX, 2.323458f, 0);
        GameObject enemyGroup = Instantiate(enemyGroupPrefab, spawnPosition, Quaternion.identity);
        UpdateEnemyCount(enemyGroup.transform.childCount);

        // Mover el grupo hacia la izquierda hasta alcanzar el límite derecho (x = 9.88)
        while (enemyGroup.transform.position.x > 9.88f)
        {
            enemyGroup.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Iniciar el movimiento de izquierda a derecha dentro de los límites
        enemyGroup.GetComponent<EnemyGroupController>().StartMoving();

        // Esperar la duración de la primera oleada
        yield return new WaitForSeconds(waveDuration);

        // Mover la primera oleada hacia arriba para desaparecer
        StartCoroutine(MoveUpAndDestroy(enemyGroup));

        // Esperar 2 segundos antes de iniciar la siguiente oleada
        yield return new WaitForSeconds(2f);

        // Segunda oleada: columna de enemigos apareciendo desde la izquierda
        Vector3 columnSpawnPosition = new Vector3(-12f, 2.08f, -1.005571f);
        GameObject enemyColumn = Instantiate(enemyColumnPrefab, columnSpawnPosition, Quaternion.identity);
        UpdateEnemyCount(enemyColumn.transform.childCount);

        // Mover la columna hacia la derecha hasta alcanzar el borde izquierdo (x = -9.88)
        while (enemyColumn.transform.position.x < -2.89f)
        {
            enemyColumn.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Iniciar el movimiento vertical (arriba y abajo)
        enemyColumn.GetComponent<EnemyColumnController>().StartMoving();

        // Esperar la duración de la segunda oleada
        yield return new WaitForSeconds(waveDuration);

        // Mover la columna hacia la izquierda para desaparecer
        StartCoroutine(MoveLeftAndDestroy(enemyColumn));

        yield return new WaitForSeconds(2f);

        // Misma lógica, pero esta vez con ambos grupos combinados
        Vector3 combinedSpawnPosition = new Vector3(startX, 2.323458f, 0);
        GameObject combinedEnemyGroup = Instantiate(enemyGroupPrefab, combinedSpawnPosition, Quaternion.identity);
        UpdateEnemyCount(combinedEnemyGroup.transform.childCount);

        while (combinedEnemyGroup.transform.position.x > 9.88f)
        {
            combinedEnemyGroup.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            yield return null;
        }

        combinedEnemyGroup.GetComponent<EnemyGroupController>().StartMoving();

        Vector3 combinedColumnSpawnPosition = new Vector3(-12f, 2.08f, -1.005571f);
        GameObject combinedEnemyColumn = Instantiate(enemyColumnPrefab, combinedColumnSpawnPosition, Quaternion.identity);
        UpdateEnemyCount(combinedEnemyColumn.transform.childCount);

        while (combinedEnemyColumn.transform.position.x < -2.89f)
        {
            combinedEnemyColumn.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            yield return null;
        }

        combinedEnemyColumn.GetComponent<EnemyColumnController>().StartMoving();

        yield return new WaitForSeconds(waveDuration);

        StartCoroutine(MoveLeftAndDestroy(combinedEnemyColumn));
        StartCoroutine(MoveUpAndDestroy(combinedEnemyGroup));

        yield return new WaitForSeconds(2f);

        ActivateBoss();
    }

    IEnumerator MoveUpAndDestroy(GameObject enemyWave)
    {
        // Detener los disparos antes de mover hacia arriba
        if (enemyWave.TryGetComponent<EnemyGroupController>(out var groupController))
        {
            groupController.StopShooting();
        }

        while (enemyWave.transform.position.y < 10f)
        {
            enemyWave.transform.Translate(Vector3.up * 5f * Time.deltaTime);
            yield return null;
        }

        UpdateEnemyCount(-enemyWave.transform.childCount);
        Destroy(enemyWave);
    }

    IEnumerator MoveLeftAndDestroy(GameObject enemyWave)
    {
        // Detener los disparos antes de mover hacia la izquierda
        if (enemyWave.TryGetComponent<EnemyColumnController>(out var columnController))
        {
            columnController.StopShooting();
        }

        while (enemyWave.transform.position.x > -15f)
        {
            enemyWave.transform.Translate(Vector3.left * 5f * Time.deltaTime);
            yield return null;
        }

        UpdateEnemyCount(-enemyWave.transform.childCount);
        Destroy(enemyWave);
    }

    void UpdateEnemyCount(int count)
    {
        activeEnemies += count;
        enemyCounterText.text = "Enemigos: " + activeEnemies;
    }

    void ActivateBoss()
    {
        if (boss != null)
        {
            boss.SetActive(true);
            boss.GetComponent<BossShooting>().ActivateBoss();
            UpdateEnemyCount(1);
        }
    }
}
