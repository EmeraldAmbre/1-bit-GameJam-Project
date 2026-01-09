using UnityEngine;

public class LampVisibilityZone : MonoBehaviour
{
    [SerializeField] private PlayerLamp _lamp;

    [Header("Visibility Radius")]
    [SerializeField] private float _minScale = 5f;
    [SerializeField] private float _maxScale = 15f;

    private void Awake()
    {
        if (_lamp == null)
            _lamp = GetComponentInParent<PlayerLamp>();
    }

    private void Start()
    {
        UpdateVisibility();
    }

    private void Update()
    {
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        float t = _lamp.NormalizedIntensity;

        float scale = Mathf.Lerp(_minScale, _maxScale, t);
        transform.localScale = Vector3.one * scale;
    }
}
