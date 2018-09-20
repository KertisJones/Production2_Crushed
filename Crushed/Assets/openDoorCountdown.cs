using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class openDoorCountdown : MonoBehaviour {
    public float timeLeft = 10f;
    public GameObject door;
    public GameObject text;

    private bool startTimer = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (startTimer)
        if (startTimer)
        {
            if (timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;

                if (timeLeft < 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
        else
        {
            if (timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;
                text.gameObject.GetComponent<Text>().text = "Time Left:" + "\n" + Mathf.Ceil(timeLeft);
                //text.gameObject.GetComponent<Text>().text = "Stand Here!" + "\n" + Mathf.Ceil(timeLeft);
                if (timeLeft - 0.8 <= 0)
                {
                    //Destroy(door);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Fading>().BeginFade(1);
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    timeLeft = 0.8f;
                    startTimer = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            startTimer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            startTimer = false;
        }
    }

}
