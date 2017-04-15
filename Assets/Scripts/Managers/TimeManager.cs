using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TimeManager : NetworkBehaviour {
    public int m_gameTime = 75;
    [SyncVar]
    public int m_currentTime;
    public int m_countDown = 3;
    [SyncVar]
    public int m_currentCountDown;

    [SyncVar]
    private bool m_gameOver = true;
    [SyncVar]
    private bool m_showWinner = false;

	void Start ()
    {
        m_currentCountDown = m_countDown;
        m_currentTime = m_gameTime;
    }

    void Update()
    {
        if(m_gameOver && m_currentCountDown < 1)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Button_StartGame()
    {
        if (isServer)
            StartCoroutine(CountDown());
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
            StartCoroutine(ShowWinner());
        }
    }

    public bool IsGameOver()
    {
        return m_gameOver;
    }

    private IEnumerator ShowWinner()
    {
        // Should be dynamic based on how many presents left to deposit.
        yield return new WaitForSeconds(5);
        m_showWinner = true;
    }

    public bool CanShowWinner()
    {
        return m_showWinner;
    }
}
