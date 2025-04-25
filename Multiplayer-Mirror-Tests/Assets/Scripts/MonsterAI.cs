using Mirror;
using UnityEngine;
using System.Linq;

public class MonsterAI : NetworkBehaviour
{
    public float moveSpeed = 3f;
    private Transform targetPlayer;

    void Update()
    {
        if (!isServer) return; // Solo el servidor controla la IA

        FindClosestPlayer();
        MoveTowardTarget();
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            targetPlayer = null;
            return;
        }

        GameObject closest = players
            .OrderBy(p => Vector3.Distance(transform.position, p.transform.position))
            .FirstOrDefault();

        if (closest != null)
        {
            targetPlayer = closest.transform;
        }
    }

    void MoveTowardTarget()
    {
        if (targetPlayer == null) return;

        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D other)
    {
        NetworkIdentity identity = other.GetComponent<NetworkIdentity>();
        if (identity != null && identity.CompareTag("Player"))
        {
            PlayerHealth health = identity.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(25);
            }
        }
    }
}