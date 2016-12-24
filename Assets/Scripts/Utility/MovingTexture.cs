using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTexture : MonoBehaviour {

    private Material m_material;
    public float m_speed = 5.0f;

	// Use this for initialization
	void Start () {
        m_material = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = m_material.mainTextureOffset;
        offset.x -= Time.deltaTime * m_speed;
        m_material.mainTextureOffset = offset;
	}
}
