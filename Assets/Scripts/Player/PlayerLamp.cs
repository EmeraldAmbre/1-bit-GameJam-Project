using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class PlayerLamp : MonoBehaviour
{
    [SerializeField] private int _maxState = 4;
    [SerializeField] private int _currentState = 0;

    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _currentEnergy = 100f;

    public float NormalizedEnergy => _currentEnergy / _maxEnergy;

    public event Action<float> OnEnergyChanged;
    public event Action<int> OnLampStateChanged;


    public int CurrentState => _currentState;
    public int MaxState => _maxState;

    public float NormalizedIntensity => 1f - (float)_currentState / _maxState;

    public float EnergyDrainMultiplier => Mathf.Lerp(2f, 0.2f, (float)_currentState / _maxState);

    public void ToggleIntensity()
    {
        _currentState = (_currentState + 1) % (_maxState + 1);
        OnLampStateChanged?.Invoke(_currentState);
    }


    public void Consume(float amount)
    {
        _currentEnergy = Mathf.Max(0f, _currentEnergy - amount);
        OnEnergyChanged?.Invoke(_currentEnergy / _maxEnergy);
    }

    private void Update()
    {
        float drain = EnergyDrainMultiplier * Time.deltaTime;

        Consume(drain);
    }
}
 