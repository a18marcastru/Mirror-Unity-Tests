using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float detectionRadius = 10f;
    public float loudnessThreshold = 0.1f;

    private void Update()
    {
        // Aqu� suponemos que el enemigo se mueve hacia el jugador si detecta ruido
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRadius)
        {
            // El jugador est� dentro del rango de detecci�n
            // Aseg�rate de que el volumen est� por encima del umbral
            if (loudnessThreshold > 0.1f)
            {
                // L�gica para hacer que el enemigo persiga al jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * Time.deltaTime * 3f; // Velocidad de movimiento
            }
        }
    }
}
