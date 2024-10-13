using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AIAudio : MonoBehaviour
{
    [SerializeField] AudioClip sfx_onSpawn;
    
    AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

        float defaultPitch = m_AudioSource.pitch;
        m_AudioSource.pitch = Random.Range(-1f, 3f);
        m_AudioSource.PlayOneShot(sfx_onSpawn, .3f);
        m_AudioSource.pitch = defaultPitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
