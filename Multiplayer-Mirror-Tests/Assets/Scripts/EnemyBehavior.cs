using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float detectionRadius = 10f;
    public float loudnessThreshold = 0.1f;

    private void Update()
    {
        // Aquí suponemos que el enemigo se mueve hacia el jugador si detecta ruido
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRadius)
        {
            // El jugador está dentro del rango de detección
            // Asegúrate de que el volumen esté por encima del umbral
            if (loudnessThreshold > 0.1f)
            {
                // Lógica para hacer que el enemigo persiga al jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * Time.deltaTime * 3f; // Velocidad de movimiento
            }
        }
    }
}
