using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObstacleLauncher : NetworkBehaviour {

    public GameObject[] m_obstacle;
    public Vector3 m_direction = Vector3.right;
    public int m_minTime, m_maxTime;

    public bool isNetWork = false;

	void Start () {
        if (!isNetWork)
        {
            int rand = Random.Range(m_minTime, m_maxTime);
            StartCoroutine(SpawnObstacle(rand));
        }
    }

    public override void OnStartServer()
    {
        int rand = Random.Range(m_minTime, m_maxTime);
        StartCoroutine(SpawnObstacle(rand));
    }

    private IEnumerator SpawnObstacle(float waitTime)
    {
        if (!isNetWork)
        {
            yield return new WaitForSeconds(waitTime);
            int rand = Random.Range(0, m_obstacle.Length);
            GameObject newObstacle = Instantiate(m_obstacle[rand], transform.position, Quaternion.identity);
            newObstacle.GetComponent<Obstacle>().m_direction = m_direction;
            rand = Random.Range(m_minTime, m_maxTime);
            StartCoroutine(SpawnObstacle(rand));
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
            int rand = Random.Range(0, m_obstacle.Length);
            GameObject newObstacle = Instantiate(m_obstacle[rand], transform.position, Quaternion.identity) as GameObject;
            newObstacle.GetComponent<Obstacle>().m_direction = m_direction;
            NetworkServer.Spawn(newObstacle);
            rand = Random.Range(m_minTime, m_maxTime);
            StartCoroutine(SpawnObstacle(rand));
        }
    }
}
