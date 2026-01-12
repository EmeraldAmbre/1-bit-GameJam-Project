using UnityEngine;

public class LanternTutorialUI : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void OnOkPressed()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
