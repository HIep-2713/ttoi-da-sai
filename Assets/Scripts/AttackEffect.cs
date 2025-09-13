using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public float knockbackForce = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // N?u d�nh Enemy
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                Vector2 hitDir = (other.transform.position - transform.position).normalized;
                enemy.Knockback(hitDir, knockbackForce); // g?i Knockback c� force
            }
        }

        // N?u d�nh Player (trong tr??ng h?p c� trap, ho?c enemy ?�nh ng??c l?i)
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Vector2 hitDir = (other.transform.position - transform.position).normalized;
                player.Knockback(hitDir, knockbackForce); // g?i Knockback c� force
            }
        }
    }
}
