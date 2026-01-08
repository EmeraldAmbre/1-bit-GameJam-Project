using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Mental Damage (%)")]
    [SerializeField, Range(0f, 100f)]
    protected float _detectionZoneDamage = 2f;

    [SerializeField, Range(0f, 100f)]
    protected float _contactDamage = 10f;

    protected MentalHealth playerMental;

    protected internal void ApplyDetectionDamage(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerManager.Instance.TryApplyMentalDamagePercent(_detectionZoneDamage);
    }

    protected internal void ApplyContactDamage(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerManager.Instance.TryApplyMentalDamagePercent(_contactDamage);
    }
}
