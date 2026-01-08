using UnityEngine;

public class MentalHealthGaugeUI : BaseGaugeUI
{
    private MentalHealth _mentalHealth;

    public void Bind(MentalHealth mentalHealth)
    {
        _mentalHealth = mentalHealth;

        SetValue(mentalHealth.NormalizedMentalHealth);
        mentalHealth.OnMentalHealthChanged += SetValue;
    }

    private void OnDestroy()
    {
        if (_mentalHealth != null)
            _mentalHealth.OnMentalHealthChanged -= SetValue;
    }
}
