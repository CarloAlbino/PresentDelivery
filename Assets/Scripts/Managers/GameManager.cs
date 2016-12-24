using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public int[] m_Points = new int[4];
    private string[] m_lastItem = new string[4];
    private int[] m_currentMultiplier = new int[4];
    public int m_maxMultiplier = 5;
    public Text[] m_multiplierText = new Text[4];
    private AudioSource m_audioSource;

	void Start ()
    {
        m_audioSource = GetComponent<AudioSource>();
		for(int i = 0; i < m_Points.Length; i++)
        {
            m_Points[i] = 0;
            m_lastItem[i] = " ";
            m_currentMultiplier[i] = 1;
        }
	}

    void Update()
    {
        for(int i = 0; i < m_multiplierText.Length; i++)
        {
            if (m_currentMultiplier[i] > 1)
            {
                m_multiplierText[i].text = "x" + m_currentMultiplier[i];
            }
            else
            {
                m_multiplierText[i].text = " "; 
            }
        }
    }

    public void AddPoints(int player, int points, string itemName)
    {
        int playerNum = player - 1;
        //Debug.Log("P" + player + " points added " + points + " last item name " + m_lastItem[playerNum] + " itemName " + itemName);
        if(itemName == m_lastItem[playerNum] && m_currentMultiplier[playerNum] < m_maxMultiplier)
        {
            m_currentMultiplier[playerNum]++;
        }
        else
        {
            m_currentMultiplier[playerNum] = 1;
        }
        m_lastItem[playerNum] = itemName;
        m_Points[playerNum] += points * m_currentMultiplier[playerNum];

        m_audioSource.Play();
    }
}
