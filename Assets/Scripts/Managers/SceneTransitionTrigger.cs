using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Scene Transition")]
    [SerializeField] private bool loadNextScene = true;
    [SerializeField] private string sceneName;

    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        _triggered = true;

        if (loadNextScene)
        {
            LoadNextScene();
        }
        else
        {
            LoadSceneByName();
        }
    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    private void LoadSceneByName()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("SceneTransitionTrigger: sceneName is empty");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}
