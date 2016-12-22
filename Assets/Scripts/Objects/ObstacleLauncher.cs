using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLauncher : MonoBehaviour {

    public Obstacle[] m_obstacle;
    public Vector3 m_direction = Vector3.right;
    public int m_minTime, m_maxTime;

	void Start () {
        int rand = Random.Range(m_minTime, m_maxTime);
        StartCoroutine(SpawnObstacle(rand));
    }

    private IEnumerator SpawnObstacle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int rand = Random.Range(0, m_obstacle.Length);
        Obstacle newObstacle = Instantiate(m_obstacle[rand], transform.position, Quaternion.identity) as Obstacle;
        newObstacle.m_direction = m_direction;
        rand = Random.Range(m_minTime, m_maxTime);
        StartCoroutine(SpawnObstacle(rand));
    }
}
