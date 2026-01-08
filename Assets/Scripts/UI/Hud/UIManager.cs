using UnityEngine;

public class UIManager : MonoBehaviour
{
    //mettre à jour manuellement dans l'inspecteur
    [Header("Player")]
    [SerializeField] private PlayerLamp _playerLamp;
    [SerializeField] private MentalHealth _mentalHealth;

    [Header("UI")]
    [SerializeField] private EnergyGaugeUI _energyGauge;
    [SerializeField] private MentalHealthGaugeUI _mentalGauge;

    private void Start()
    {
        BindPlayerUI();
    }

    private void BindPlayerUI()
    {
        if (_playerLamp == null || _energyGauge == null)
        {
            Debug.LogError("UIManager: Missing references");
            return;
        }

        _energyGauge.Bind(_playerLamp);

        if (_mentalHealth == null || _mentalGauge == null)
        {
            Debug.LogError("UIManager: Missing references");
            return;
        }

        _mentalGauge.Bind(_mentalHealth);
    }
}
