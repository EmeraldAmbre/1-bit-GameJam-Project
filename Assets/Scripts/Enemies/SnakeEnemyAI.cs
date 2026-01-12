using UnityEngine;

public class SnakeEnemyAI : BaseEnemy
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _patrolDistance = 3f;

    private Rigidbody2D _rb;
    private Vector2 _origin;
    private int _direction = 1;

    private SpriteRenderer _spriteRenderer;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _origin = transform.position;
    }

    private void FixedUpdate()
    {
        float offset = transform.position.x - _origin.x;

        if (Mathf.Abs(offset) >= _patrolDistance)
        {
            _direction *= -1;
            Flip();
        }

        _rb.linearVelocity = new Vector2(_direction * _moveSpeed, _rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (_spriteRenderer == null)
            return;

        _spriteRenderer.flipX = _direction < 0;
    }
}
