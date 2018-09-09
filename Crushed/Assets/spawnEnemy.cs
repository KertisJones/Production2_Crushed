using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour {
    public GameObject wall;
    public GameObject enemy;
    private Transform playerTrans;
    private Transform finishTrans;

    public float minDistPlayer = 1f;
    public float minDistFinish = 1f;
    public float rechargeTime = 0.5f;
    private float timeLeft;

	// Use this for initialization
	void Start () {
        timeLeft = 0;
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        finishTrans = GameObject.FindGameObjectWithTag("Finish").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Trying to spawn...");
            Vector3 spawnPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (Vector3.Distance(spawnPoint, playerTrans.position) >= minDistPlayer && Vector3.Distance(spawnPoint, finishTrans.position) >= minDistFinish && timeLeft < 0)
            {
                Instantiate(enemy, spawnPoint, transform.rotation);
                timeLeft = rechargeTime;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            //Debug.Log("Trying to spawn...");
            Vector3 spawnPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (Vector3.Distance(spawnPoint, playerTrans.position) >= minDistPlayer && Vector3.Distance(spawnPoint, finishTrans.position) >= minDistFinish && timeLeft < 0)
            {
                Instantiate(wall, spawnPoint, transform.rotation);
                timeLeft = rechargeTime;
            }
        }
    }
}
