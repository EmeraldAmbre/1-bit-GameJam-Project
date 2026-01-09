using UnityEngine;

public class BatEnemyAI : BaseEnemy
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _oscillationAmplitude = 1f;
    [SerializeField] private float _oscillationFrequency = 2f;
    [SerializeField] private float _patrolDistance = 5f;

    private Vector2 _origin;
    private int _direction = 1;

    [Header("Aggro")]
    [SerializeField] private float _chaseStrength = 2f;

    private Rigidbody2D _rb;
    private float _time;

    private Transform _player;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _origin = transform.position;
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        Vector2 oscillation = GetOscillation();
        Vector2 chase = GetChaseDirection();

        Vector2 velocity = (oscillation + chase).normalized * _moveSpeed;
        _rb.linearVelocity = velocity;

        if (_rb.linearVelocity.x != 0)
        {
            _spriteRenderer.flipX = _rb.linearVelocity.x < 0;
        }
    }

    private Vector2 GetOscillation()
    {
        float xOffset = transform.position.x - _origin.x;

        if (Mathf.Abs(xOffset) >= _patrolDistance)
            _direction *= -1;

        float y = Mathf.Sin(_time * _oscillationFrequency) * _oscillationAmplitude;

        return new Vector2(_direction, y);
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
}
