using UnityEngine;

public class PlayerPersistentData : MonoBehaviour
{
    public static PlayerPersistentData Instance { get; private set; }

    [Header("Mental Health")]
    public float CurrentMentalHealth = 100f;

    [Header("Lantern")]
    public float CurrentLanternEnergy = 100f;
    public int LanternState = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
