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

	void Start ()
    {
        m_timeManager = FindObjectOfType<TimeManager>();
        m_gameManager = FindObjectOfType<GameManager>();
	}
	
	void Update ()
    {
		for(int i = 0; i < m_score.Length; i++)
        {
            m_score[i].text = m_gameManager.m_Points[i].ToString();
        }

        m_Time.text = m_timeManager.m_currentTime.ToString();

        if (m_timeManager.m_currentCountDown > 1)
            m_countDown.text = "READY?";
        else if (m_timeManager.m_currentCountDown > 0)
            m_countDown.text = "SET.";
        else if (m_timeManager.m_currentTime > m_timeManager.m_gameTime - 1)
            m_countDown.text = "GO!";
        else if (m_timeManager.m_currentTime <= 0)
            m_countDown.text = "FINISH!";
        else
            m_countDown.text = "";
	}

}
