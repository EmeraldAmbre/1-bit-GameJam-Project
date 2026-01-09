using UnityEngine;

public class LampVisibilityZone : MonoBehaviour
{
    [SerializeField] private PlayerLamp _lamp;

    [Header("Radius Settings")]
    [SerializeField] private float _maxRadius = 6f;
    [SerializeField] private float _minRadius = 1.5f;

    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();

        if (_lamp == null)
            _lamp = GetComponent<PlayerLamp>();
    }

    private void OnEnable()
    {
        _lamp.OnLampStateChanged += UpdateRadius;
        UpdateRadius(_lamp.CurrentState); // synchro initiale
    }

    private void OnDisable()
    {
        _lamp.OnLampStateChanged -= UpdateRadius;
    }

    private void UpdateRadius(int state)
    {
        float t = (float)state / _lamp.MaxState;
        _collider.radius = Mathf.Lerp(_maxRadius, _minRadius, t);
    }
}
