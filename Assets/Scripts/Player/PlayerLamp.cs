using System;
using UnityEngine;

public class PlayerLamp : MonoBehaviour
{
    [SerializeField] private int _maxState = 4;
    [SerializeField] private int _currentState = 0;

    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _currentEnergy = 100f;

    public float CurrentEnergy => _currentEnergy;
    public float NormalizedEnergy => _currentEnergy / _maxEnergy;

    public event Action<float> OnEnergyChanged;
    public event Action<int> OnLampStateChanged;


    public int CurrentState => _currentState;
    public int MaxState => _maxState;

    public float NormalizedIntensity => 1f - (float)_currentState / _maxState;

    public float EnergyDrainMultiplier => Mathf.Lerp(2f, 0.2f, (float)_currentState / _maxState);

    public void ToggleIntensity()
    {
        if (_currentEnergy <= 0f)
            return;

        _currentState = (_currentState + 1) % (_maxState + 1);

        SoundManager.Instance.PlayTimedSound("character_lantern2", duration: 3f, fadeOutTime: 0.5f);

        OnLampStateChanged?.Invoke(_currentState);
    }

    public void SetEnergy(float value)
    {
        _currentEnergy = Mathf.Clamp(value, 0f, _maxEnergy);
        OnEnergyChanged?.Invoke(_currentEnergy / _maxEnergy);
    }

    public void SetState(int state)
    {
        _currentState = Mathf.Clamp(state, 0, _maxState);
    }

    public void Consume(float amount)
    {
        _currentEnergy = Mathf.Max(0f, _currentEnergy - amount);

        if (_currentEnergy <= 0f)
        {
            ForceLowestIntensity();
        }

        OnEnergyChanged?.Invoke(_currentEnergy / _maxEnergy);
    }

    private void ForceLowestIntensity()
    {
        _currentState = _maxState;
    }

    private void Update()
    {
        float drain = EnergyDrainMultiplier * Time.deltaTime;

        Consume(drain);
    }
}
 