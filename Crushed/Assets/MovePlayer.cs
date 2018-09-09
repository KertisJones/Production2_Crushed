using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    public float speed = 5f;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		x = x * Time.deltaTime * speed;
		y = y * Time.deltaTime * speed;

		this.GetComponent<Rigidbody2D>().transform.Translate (x, y, 0f);
	}
}
