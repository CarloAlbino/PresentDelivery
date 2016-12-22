using UnityEngine;
using System.Collections;

public class FaceDirection : AbstractBehaviour {

    public GameObject m_childMesh = null;
    public float m_rotateSpeed = 5.0f;
	
	void Update () 
    {
        if (m_inputState.absVelX > 0.1f || m_inputState.absVelZ > 0.1f)
        {
            if (m_childMesh != null)
            {
                Vector3 velocity = m_rb.velocity;

                float angle = Vector3.Angle(transform.forward.normalized, velocity.normalized);

                if(angle < 2.0f)
                    m_childMesh.transform.Rotate(Vector3.up, angle * Time.deltaTime * m_rotateSpeed);
            }
            else
            {
                Debug.LogError("No child object connected to FaceDirection!");
            }
        }
	}
}
