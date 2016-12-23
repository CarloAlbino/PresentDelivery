using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text[] m_score = new Text[4];
    public Text m_Time;
    public Text m_countDown;

    private TimeManager m_timeManager;
    private GameManager m_gameManager;
    private AudioManager m_audioManager;

	void Start ()
    {
        m_timeManager = FindObjectOfType<TimeManager>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_audioManager = FindObjectOfType<AudioManager>();
	}
	
	void Update ()
    {
		for(int i = 0; i < m_score.Length; i++)
        {
            m_score[i].text = m_gameManager.m_Points[i].ToString();
        }

        m_Time.text = m_timeManager.m_currentTime.ToString();

        // This if statement needs to be replaced with something better

        if (m_timeManager.m_currentCountDown > 1)
        {
            m_countDown.text = "READY?";
            m_audioManager.PlayCountDown(0);
        }
        else if (m_timeManager.m_currentCountDown > 0)
        {
            m_countDown.text = "SET.";
            m_audioManager.PlayCountDown(1);
        }
        /*else if (m_timeManager.m_currentTime < m_timeManager.m_gameTime - 60 && m_timeManager.m_currentTime > m_timeManager.m_gameTime - 61)
        {
            m_countDown.text = "HURRY UP!";
            m_audioManager.PlayCountDown(3);
        }*/
        else if (m_timeManager.m_currentTime > m_timeManager.m_gameTime - 1)
        {
            m_countDown.text = "GO!";
            m_audioManager.PlayCountDown(2);
            m_audioManager.PlayBackgroundMusic();
        }
        else if (m_timeManager.CanShowWinner())
            m_countDown.text = "The winner is " + GetWinner() + "! \n---\nPRESS SPACE TO CONTINUE";
        else if (m_timeManager.m_currentTime <= 0)
            m_countDown.text = "FINISH!";
        else
        {
            m_countDown.text = "";
        }
	}

    private string GetWinner()
    {
        string winner = "";
        int topScore = -1;
        int player = -1;
        for(int i = 0; i < m_score.Length; i++)
        {
            if(m_gameManager.m_Points[i] > topScore)
            {
                topScore = m_gameManager.m_Points[i];
                player = i;
            }
            else if(m_gameManager.m_Points[i] == topScore && player < 10)
            {
                player += i * 10;
            }else if(m_gameManager.m_Points[i] == topScore && player < 100)
            {
                player += i * 100;
            }
            else if (m_gameManager.m_Points[i] == topScore && player < 1000)
            {
                player += i * 1000;
            }
        }

        if(player > 1000)
        {
            winner = "Player 1, Player 2, Player 3 and Player 4";
        }
        else if(player > 100)
        {
            winner = "Player " + (player + 1).ToString()[2] + ", Player " + (player + 10).ToString()[1] + " and Player " + (player + 100).ToString()[0];
        }
        else if(player >= 10)
        {
            winner = "Player " + (player + 1).ToString()[1] + " and Player " + (player + 10).ToString()[0];
        }
        else
        {
            winner = "Player " + (player + 1).ToString();
        }

        return winner;
    } 

}
