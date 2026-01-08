using UnityEngine;

public class BatEnemyAI : BaseEnemy
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _oscillationAmplitude = 1f;
    [SerializeField] private float _oscillationFrequency = 2f;

    [Header("Aggro")]
    [SerializeField] private float _chaseStrength = 2f;

    private Rigidbody2D _rb;
    private float _time;

    private Transform _player;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        Vector2 oscillation = GetOscillation();
        Vector2 chase = GetChaseDirection();

        Vector2 velocity = (oscillation + chase).normalized * _moveSpeed;
        _rb.linearVelocity = velocity;
    }

    private Vector2 GetOscillation()
    {
        float y = Mathf.Sin(_time * _oscillationFrequency) * _oscillationAmplitude;
        return new Vector2(1f, y);
    }

    private Vector2 GetChaseDirection()
    {
        if (_player == null)
            return Vector2.zero;

        Vector2 toPlayer = (_player.position - transform.position);
        return toPlayer.normalized * _chaseStrength;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyContactDamage(other);

        if (other.CompareTag("Player"))
            _player = other.transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _player = null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        ApplyDetectionDamage(other);
    }
}
