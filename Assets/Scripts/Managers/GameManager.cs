using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public int[] m_Points = new int[4];
    private string[] m_lastItem = new string[4];
    private int[] m_currentMultiplier = new int[4];
    public int m_maxMultiplier = 5;
	void Start ()
    {
		for(int i = 0; i < m_Points.Length; i++)
        {
            m_Points[i] = 0;
            m_lastItem[i] = " ";
            m_currentMultiplier[i] = 1;
        }
	}

    public void AddPoints(int player, int points, string itemName)
    {
        int playerNum = player - 1;
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
    }
}
