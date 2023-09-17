using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_BGM : MonoBehaviour
{
    public static SoundManager_BGM m_Instane;

    private AudioSource m_AudioSourceBGM;

    private void Awake()
    {
        if (m_Instane == null)
        {
            m_AudioSourceBGM = this.GetComponent<AudioSource>();
            m_AudioSourceBGM.Play();

            m_Instane = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        m_AudioSourceBGM = this.GetComponent<AudioSource>();
    }

    public void PlayBackGroundMusic(AudioClip audioClip_BGM)
    {
        m_AudioSourceBGM.clip = audioClip_BGM;
        m_AudioSourceBGM.Play();
    }

    public void StopBackGroundMusic()
    {
        m_AudioSourceBGM.Stop();
    }
}
