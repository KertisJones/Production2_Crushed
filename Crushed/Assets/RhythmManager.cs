﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RhythmManager : MonoBehaviour {
    public GameObject wall;
    public GameObject enemy;
    private Transform playerTrans;
    private Transform finishTrans;
    public AudioClip beatSound;
    public AudioSource muscicSource;
    public GameObject cursor;

    public float minDistPlayer = 1f;
    public float minDistFinish = 1f;

    private float timeBetweenBeats = 0.5f;
    public float delayAfterClick = 0.5f;

    public float percentagePerfect = 0.95f;
    public int perfectBeatsNeeded = 4;
    public int perfectBeats = 0;

    public int bpm = 80;

    private float timeLeft = 0;
    private float delayTimeLeft = 0;
    private float sizeModifier = 0;
    private bool clickedThisBeat = false;
    private float lastTime = 0;

    



    //the current position of the song (in seconds)
    private float songPosition;

    //how much time (in seconds) has passed since the song started
    private float dsptimesong;

    //the current position of the song (in beats)
    private float songPosInBeats;

    // Use this for initialization
    void Awake () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        finishTrans = GameObject.FindGameObjectWithTag("Finish").transform;

        timeBetweenBeats = 0.75f;

        //record the time when the song starts
        dsptimesong = (float)AudioSettings.dspTime;

        //start the song
        muscicSource.Play();

        // EXAMPLE A: initialize with the preferences set in DOTween's Utility Panel
        DOTween.Init();
        // EXAMPLE B: initialize with custom settings, and set capacities immediately
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);

        songPosInBeats = songPosition / timeBetweenBeats;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        cursor.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        Cursor.visible = false;

        //Debug.Log(songPosition % timeBetweenBeats);
        if ((songPosition % timeBetweenBeats > 0.01 && timeLeft > 0) || timeLeft > timeBetweenBeats - 0.1)
        {
            timeLeft -= muscicSource.time - lastTime;
            delayTimeLeft -= muscicSource.time - lastTime;

            lastTime = muscicSource.time;

            
            //sizeModifier = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * (180 * (timeBetweenBeats / timeLeft))));
            if (timeLeft >= timeBetweenBeats / 2)
            {
                sizeModifier = 2 * ((timeLeft - (timeBetweenBeats / 2)) / timeBetweenBeats);
            }
            else
            {
                sizeModifier = 2 * (((timeBetweenBeats / 2) - timeLeft) / timeBetweenBeats);
            }

            cursor.GetComponent<SpriteRenderer>().material.DOColor(Color.white, timeBetweenBeats * 2f);

        }
        else
        {
            //Debug.Log(timeLeft);
            timeLeft = timeBetweenBeats;
            timeLeft -= muscicSource.time - lastTime;
            delayTimeLeft -= muscicSource.time - lastTime;

            lastTime = muscicSource.time;
            AudioSource.PlayClipAtPoint(beatSound, new Vector3(0f, 0f, -10f));
            cursor.GetComponent<SpriteRenderer>().material.DOColor(Color.black, timeBetweenBeats * 0.25f);
        }


        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Trying to spawn...");
            Vector3 spawnPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (Vector3.Distance(spawnPoint, playerTrans.position) >= minDistPlayer && delayTimeLeft < 0)
            {
                //Debug.Log("Attempting to spawn...");
                Spawn(spawnPoint);
            }
            else
            {
                //Debug.Log("Couldn't spawn.");
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Spawn(Vector3 spawnPoint)
    {
        delayTimeLeft = delayAfterClick;

        if (sizeModifier > percentagePerfect)
        {
            perfectBeats += 1;
            //Debug.Log(perfectBeats);
        }
        else
        {
            perfectBeats = 0;
        }

        if (perfectBeats == perfectBeatsNeeded)
        {
            perfectBeats = 0;
            Instantiate(enemy, spawnPoint, transform.rotation);
            //Debug.Log("Spawning enemy.");
        }
        else
        {
            //Debug.Log("Spawning block.");
            float newSize = 0.25f + (2.75f * sizeModifier);
            GameObject block = Instantiate(wall, spawnPoint, transform.rotation) as GameObject;
            block.transform.localScale = new Vector3(newSize, newSize, newSize);
            if (sizeModifier > percentagePerfect)
            {
                var main = block.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                main.startColor = Color.red;
                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
                var col = block.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
                col.color = grad;
            }
        }
    }
}
