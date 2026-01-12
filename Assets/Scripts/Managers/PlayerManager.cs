using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Mental Damage Cooldown")]
    [SerializeField] private float mentalInvincibilityDuration = 0.5f;

    private float _lastMentalDamageTime = -999f;
    private MentalHealth _mentalHealth;
    private SpriteRenderer _spriteRenderer;

    public bool IsPlayerDead;

    public event System.Action OnPlayerDied;

    private void Awake()
    {
        IsPlayerDead = false;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _mentalHealth = GetComponent<MentalHealth>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (IsPlayerDead)
            return;

        if (_mentalHealth != null && _mentalHealth.IsEmpty)
        {
            Die();
        }
    }

    public void TryApplyMentalDamagePercent(float percent)
    {
        if (Time.time - _lastMentalDamageTime < mentalInvincibilityDuration)
            return;

        _lastMentalDamageTime = Time.time;

        _mentalHealth.TakeDamagePercent(percent);

        if (_spriteRenderer != null) StartCoroutine(Blink());
    }

    private System.Collections.IEnumerator Blink()
    {
        float duration = mentalInvincibilityDuration;
        float interval = 0.1f;
        float t = 0f;

        while (t < duration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(interval);
            t += interval;
        }

        _spriteRenderer.enabled = true;
    }

    private void Die()
    {
        IsPlayerDead = true;
        Time.timeScale = 0f;
        OnPlayerDied?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
