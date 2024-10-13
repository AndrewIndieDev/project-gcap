using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOnNumberTick : MonoBehaviour
{
    [SerializeField] AudioClip SFX_onAddedPoint;
    [SerializeField] float volume = .1f;
    AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        PointSystem.onAddedPoints += PlayAudio;
    }

    void PlayAudio()
    {
        m_AudioSource.PlayOneShot(SFX_onAddedPoint, volume);
    }
}
