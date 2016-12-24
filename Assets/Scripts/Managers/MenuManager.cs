using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject[] m_panels;
    private int m_currentPanel = 0;

    void Start()
    {
        GoToPanel(m_currentPanel);
    }

    void Update()
    {
        // This needs to be more generic in the future
        // In main menu
        if (m_currentPanel == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GoToPanel(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GoToPanel(3);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                QuitGame();
            }
        }
        // In Instructions 1
        else if (m_currentPanel == 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GoToPanel(2);
            }
        }
        // In Instructions 2
        else if (m_currentPanel == 2)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GoToPanel(0);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GoToPanel(1);
            }
        }
        // In Credits
        else if (m_currentPanel == 3)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GoToPanel(0);
            }
        }
    }

    public void GoToLevel(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToPanel(int num)
    {
        for(int i = 0; i < m_panels.Length; i++)
        {
            m_panels[i].SetActive(false);
        }
        m_panels[num].SetActive(true);
        m_currentPanel = num;
    }

}
