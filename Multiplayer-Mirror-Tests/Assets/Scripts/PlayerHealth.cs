using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    public int currentHealth = 100;

    public int maxHealth = 100;

    [Header("UI")]
    public Image healthBarFill;
    
    // Referencia al canvas de la barra de salud
    public GameObject healthBarCanvas;

    void Start()
    {
        // Asegurarse de que la barra de salud comience con el valor correcto
        UpdateHealthBar();
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        // Configurar la UI para este cliente
        SetupHealthUI();
    }

    void SetupHealthUI()
    {
        // Si no es el jugador local, ocultar su UI de salud
        if (!isLocalPlayer && healthBarCanvas != null)
        {
            healthBarCanvas.SetActive(false);
        }
    }

    // Hook que se llama cuando cambia la vida
    void OnHealthChanged(int oldHealth, int newHealth)
    {
        // Solo actualizar la UI si este es nuestro jugador local
        if (isLocalPlayer)
        {
            UpdateHealthBar();
        }
    }

    [Server]
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            // Lógica de muerte
            Debug.Log($"{gameObject.name} ha muerto.");
            // Opcionalmente implementar respawn o eliminación del jugador
            RpcPlayerDied();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
    
    [ClientRpc]
    void RpcPlayerDied()
    {
        gameObject.SetActive(false);
    }
}