using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10);
    [SerializeField] private float _speed = 10f;

    private void Awake()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;

        Vector3 targetPosition = _player.position + _offset;

        float t = 1f - Mathf.Exp(-_speed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, t);
    }
}
