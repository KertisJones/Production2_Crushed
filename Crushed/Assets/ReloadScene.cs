using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ReloadScene : MonoBehaviour {
    public float timeLeft = 0f;

    private bool startTimer = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            startTimer = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().DOShakePosition(timeLeft, 1, 10, 90, true);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Fading>().BeginFade(1);
        }
    }
}
