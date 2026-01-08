using UnityEngine;

public class EnergyGaugeUI : BaseGaugeUI
{
    private PlayerLamp _lamp;

    public void Bind(PlayerLamp lamp)
    {
        _lamp = lamp;

        SetValue(lamp.NormalizedEnergy);

        lamp.OnEnergyChanged += SetValue;
    }

    private void OnDestroy()
    {
        if (_lamp != null)
            _lamp.OnEnergyChanged -= SetValue;
    }
}
