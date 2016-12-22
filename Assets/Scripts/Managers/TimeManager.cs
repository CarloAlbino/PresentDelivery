using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
    public int m_gameTime = 75;
    private int m_currentTime;
    public int m_countDown = 3;
    private int m_currentCountDown;

    private bool m_gameOver = true;

	void Start ()
    {
        m_currentCountDown = m_countDown;
        m_currentTime = m_gameTime;
	}

    private IEnumerator CountDown()
    {
        m_currentCountDown--;
        yield return new WaitForSeconds(1);
        if(m_currentCountDown > 0)
        {
            StartCoroutine(CountDown());
        }
        else
        {
            m_gameOver = false;
            StartCoroutine(GameTime());
        }
    }

    private IEnumerator GameTime()
    {
        m_currentTime--;
        yield return new WaitForSeconds(1);
        if(m_currentTime > 0)
        {
            StartCoroutine(GameTime());
        }
        else
        {
            m_gameOver = true;
        }
    }
}
