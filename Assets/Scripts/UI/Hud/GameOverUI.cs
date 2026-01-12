using UnityEngine;
using UnityEngine.Rendering;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    private void Awake()
    {
        _panel.SetActive(false);
    }

    private void Start()
    {
        PlayerManager.Instance.OnPlayerDied += Show;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnPlayerDied -= Show;
    }

    private void Show()
    {
        _panel.SetActive(true);
    }

    public void Restart()
    {
        PlayerManager.Instance.RestartGame();
    }
}
