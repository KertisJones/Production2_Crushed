using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreRoof : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Roof").GetComponent<BoxCollider2D>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
