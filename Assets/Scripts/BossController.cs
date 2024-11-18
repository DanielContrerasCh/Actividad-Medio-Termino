using UnityEngine;

public class BossController : MonoBehaviour
{
    public int bossHealth = 100;
    public Vector3 originalScale;
    public float minScaleFactor = 0.3f; // Escala mínima (30% del tamaño original)

    void Start()
    {
        // Guardar la escala original del jefe
        originalScale = transform.localScale;
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;

        // Reducir el tamaño del jefe basado en su salud
        UpdateBossScale();

        if (bossHealth <= 0)
        {
            GameManager.Instance.EndGame("You defeated evil Star Link", gameObject);
        }
    }

    void UpdateBossScale()
    {
        // Calcular el porcentaje de vida restante
        float healthPercentage = Mathf.Clamp01((float)bossHealth / 100f);

        // Calcular el nuevo factor de escala basado en la vida restante
        float scaleFactor = Mathf.Lerp(minScaleFactor, 1f, healthPercentage);

        // Ajustar la escala del jefe
        transform.localScale = originalScale * scaleFactor;
    }
}
