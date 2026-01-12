using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerManager.Instance.OnPlayerDied += Show;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnPlayerDied -= Show;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        PlayerManager.Instance.RestartGame();
    }
}
