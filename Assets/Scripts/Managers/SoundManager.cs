using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _audioSource;
    private Dictionary<string, AudioClip> _clipByName;
    [SerializeField] private List<AudioClip> _soundList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
        BuildDictionary();
    }

    private void BuildDictionary()
    {
        _clipByName = new Dictionary<string, AudioClip>();

        foreach (var clip in _soundList)
        {
            if (clip == null)
                continue;

            if (_clipByName.ContainsKey(clip.name))
            {
                Debug.LogWarning($"Duplicate AudioClip name: {clip.name}");
                continue;
            }

            _clipByName.Add(clip.name, clip);
        }
    }

    public void PlaySound(string clipName)
    {
        if (_clipByName.TryGetValue(clipName, out var clip))
        {
            _audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound '{clipName}' not found");
        }
    }
}
