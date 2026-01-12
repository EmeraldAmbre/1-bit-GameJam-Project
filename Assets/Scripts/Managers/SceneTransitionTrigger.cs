using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Scene Transition")]
    [SerializeField] private bool _loadNextScene = true;
    [SerializeField] private string _sceneName;

    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        _triggered = true;

        SavePlayerState(other);

        if (_loadNextScene)
        {
            LoadNextScene();
        }
        else
        {
            LoadSceneByName();
        }
    }

    private void SavePlayerState(Collider2D playerCollider)
    {
        var mental = playerCollider.GetComponent<MentalHealth>();
        var lamp = playerCollider.GetComponent<PlayerLamp>();
        var data = PlayerPersistentData.Instance;

        if (mental != null)
            data.CurrentMentalHealth = mental.CurrentValue;

        if (lamp != null)
        {
            data.CurrentLanternEnergy = lamp.CurrentEnergy;
            data.LanternState = lamp.CurrentState;
        }
    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == 1) OnLastSceneReached();
        else SceneManager.LoadScene(currentIndex + 1);
    }

    private void LoadSceneByName()
    {
        if (string.IsNullOrEmpty(_sceneName))
        {
            Debug.LogError("SceneTransitionTrigger: sceneName is empty");
            return;
        }

        SceneManager.LoadScene(_sceneName);
    }

    private void OnLastSceneReached()
    {
        var data = PlayerPersistentData.Instance;
        if (data != null)
        {
            data.CurrentMentalHealth = 100f;
            data.CurrentLanternEnergy = 100f;
            data.LanternState = 0;
        }

        SceneManager.LoadScene(0);
    }
}
