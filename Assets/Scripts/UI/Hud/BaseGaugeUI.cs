using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGaugeUI : MonoBehaviour
{
    protected Slider _slider;

    [Range(0f, 1f)]
    [SerializeField] protected float _currentValue = 1f;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = 0f;
        _slider.maxValue = 1f;
        _slider.interactable = false;

        UpdateVisual();
    }

    public virtual void SetValue(float value)
    {
        _currentValue = Mathf.Clamp01(value);
        UpdateVisual();
    }

    public virtual void AddValue(float delta)
    {
        SetValue(_currentValue + delta);
    }

    public float GetValue() => _currentValue;

    protected virtual void UpdateVisual()
    {
        _slider.value = _currentValue;
    }
}
