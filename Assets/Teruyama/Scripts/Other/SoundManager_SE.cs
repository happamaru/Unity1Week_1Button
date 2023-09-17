using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_SE : MonoBehaviour
{
    public static SoundManager_SE m_Instane;

    private AudioSource m_AudioSourceSE;

    private void Awake()
    {
        if (m_Instane == null)
        {
            m_Instane = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        m_AudioSourceSE = this.GetComponent<AudioSource>();
    }

    public void PlaySoundEfect(AudioClip audioClip_SE)
    {
        m_AudioSourceSE.PlayOneShot(audioClip_SE);
    }
}
