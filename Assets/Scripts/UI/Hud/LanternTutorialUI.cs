using UnityEngine;
using UnityEngine.SceneManagement;

public class LanternTutorialUI : MonoBehaviour
{
    [Header("Tutorial Settings")]
    [SerializeField] private int _showOnlyInSceneIndex = 0; // scène 1 = index 0
    private void Awake()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != _showOnlyInSceneIndex)
        {
            gameObject.SetActive(false);
            return;
        }

        Time.timeScale = 0f;
    }

    public void OnOkPressed()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
