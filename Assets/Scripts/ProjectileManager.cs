using UnityEngine;
using TMPro;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;
    public TextMeshProUGUI counterText;
    private int activeProjectiles = 0;

    void Awake()
    {
        // Asegurarnos de que solo haya una instancia del manager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementCount()
    {
        activeProjectiles++;
        UpdateCounterText();
    }

    public void DecrementCount()
    {
        activeProjectiles--;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        counterText.text = "Proyectiles: " + activeProjectiles;
    }
}
