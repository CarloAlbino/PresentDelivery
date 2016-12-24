using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAllItems : MonoBehaviour {

    private Animator m_animator;

	// Use this for initialization
	IEnumerator Start () {
        //Debug.Log("dropped");
        m_animator = GetComponent<Animator>();
        yield return new WaitForSeconds(1.0f);
        m_animator.Play("Drop");
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);

	}

}
