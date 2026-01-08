using System;
using UnityEngine;

public class MentalHealth : MonoBehaviour
{
    [SerializeField] private float _maxMentalHealth = 100f;
    [SerializeField] private float _currentMentalHealth = 100f;

    public event Action<float> OnMentalHealthChanged;

    public float NormalizedMentalHealth =>
        _maxMentalHealth <= 0f ? 0f : _currentMentalHealth / _maxMentalHealth;

    private void Start()
    {
        // synchro initiale UI
        OnMentalHealthChanged?.Invoke(NormalizedMentalHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f)
            return;

        _currentMentalHealth = Mathf.Max(0f, _currentMentalHealth - amount);
        OnMentalHealthChanged?.Invoke(NormalizedMentalHealth);
    }

    public void Heal(float amount)
    {
        if (amount <= 0f)
            return;

        _currentMentalHealth = Mathf.Min(_maxMentalHealth, _currentMentalHealth + amount);
        OnMentalHealthChanged?.Invoke(NormalizedMentalHealth);
    }

    public bool IsEmpty => _currentMentalHealth <= 0f;

    /// <summary>
    /// Debug Only
    /// </summary>
    private void OnValidate()
    {
        if (_maxMentalHealth <= 0f)
            return;

        _currentMentalHealth = Mathf.Clamp(_currentMentalHealth, 0f, _maxMentalHealth);

        OnMentalHealthChanged?.Invoke(NormalizedMentalHealth);
    }
}
