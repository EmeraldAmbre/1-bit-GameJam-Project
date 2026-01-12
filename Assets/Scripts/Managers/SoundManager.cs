using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _sfxSource;
    private AudioSource _loopSource;
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

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _loopSource = gameObject.AddComponent<AudioSource>();

        _loopSource.loop = true;
        _loopSource.playOnAwake = false;

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
            _sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound '{clipName}' not found");
        }
    }

    public void PlayLoop(string clipName)
    {
        if (_loopSource.clip != null && _loopSource.clip.name == clipName)
            return;

        if (_clipByName.TryGetValue(clipName, out var clip))
        {
            _loopSource.clip = clip;
            _loopSource.Play();
        }
    }

    public void StopLoop()
    {
        _loopSource.Stop();
        _loopSource.clip = null;
    }
}
