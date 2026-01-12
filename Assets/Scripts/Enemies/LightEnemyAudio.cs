using System.Collections.Generic;
using UnityEngine;

public class LightEnemyAudio : MonoBehaviour
{
    [System.Serializable]
    public class TagSound
    {
        public string Tag;
        public string ClipName; // nom dans SoundManager
    }

    [SerializeField] private List<TagSound> _tagSounds;

    private readonly HashSet<Collider2D> _enemiesInLight = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        string clip = GetClipForTag(other.tag);
        if (clip == null)
            return;

        _enemiesInLight.Add(other);

        SoundManager.Instance.PlayLoop(clip);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_enemiesInLight.Remove(other))
            return;

        if (_enemiesInLight.Count == 0)
            SoundManager.Instance.StopLoop();
    }

    private string GetClipForTag(string tag)
    {
        foreach (var ts in _tagSounds)
        {
            if (ts.Tag == tag)
                return ts.ClipName;
        }
        return null;
    }
}
