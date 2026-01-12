using UnityEngine;

public class DeerEnemyAI : BaseEnemy
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDetectionDamage(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyContactDamage(collision.collider);
    }
}
