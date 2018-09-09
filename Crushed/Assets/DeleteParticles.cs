using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<Rigidbody2D>().rotation != 0)
        {
            Destroy(this.GetComponent<ParticleSystem>());
        }
	}
}
