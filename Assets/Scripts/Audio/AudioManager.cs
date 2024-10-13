using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource m_MusicAudioSource;
    public AudioSource m_SFXAudioSource;
    public AudioClip SFXAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        if(m_MusicAudioSource)
            m_MusicAudioSource.Play();
        if (m_SFXAudioSource && SFXAudioClip)
            m_SFXAudioSource.PlayOneShot(SFXAudioClip);
    }
}
