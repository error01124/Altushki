using UnityEngine;

public class MusicPlayer : MonoBehaviour, IService
{
    [SerializeField] private AudioSource _audioSource;

    public void Init()
    {

    }

    public void Play(string path)
    {
        Stop();
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    public void Stop()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
