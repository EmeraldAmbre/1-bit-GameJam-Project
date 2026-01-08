using UnityEngine;

public class EnemyDetectionZone : MonoBehaviour
{
    private BaseEnemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<BaseEnemy>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _enemy.ApplyDetectionDamage(other);
    }
}
