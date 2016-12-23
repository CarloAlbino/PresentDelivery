using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public AudioClip[] m_coutdownClips;
    public AudioSource m_backgroundMusic;
    private AudioSource m_audioSource;

	void Start ()
    {
        m_audioSource = GetComponent<AudioSource>();
	}

    public void PlayCountDown(int i)
    {
        if (m_audioSource.clip != m_coutdownClips[i])
        {
            m_audioSource.clip = m_coutdownClips[i];
            m_audioSource.Play();

            /*if (i == 2)
            {
                StartCoroutine(PlayBackgroundMusic());
            }*/
        }

    }

    public void PlayBackgroundMusic()
    {
        //yield return new WaitForSeconds(2);
        //if(m_audioSource.clip != m_backgroundMusic)
        //    StartCoroutine(SetBackgroundMusic());
        if (!m_backgroundMusic.isPlaying)
            m_backgroundMusic.Play();

    }

    /*private IEnumerator SetBackgroundMusic()
    {
        yield return new WaitForEndOfFrame();
        if(!m_audioSource.isPlaying)
        {
            StartCoroutine(SetBackgroundMusic());
        }
        else
        {
            m_audioSource.clip = m_backgroundMusic;
            m_audioSource.loop = true;
            m_audioSource.Play();
        }
    }*/
}
