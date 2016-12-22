using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public Transform player;
    public float dampTime = 0.5f;
    public float yOffset = 148f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 from = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        Vector3 to = new Vector3(player.position.x, player.position.y, transform.position.z);

        transform.position = Vector3.Lerp(from, to, Time.deltaTime * dampTime);
	}
}
