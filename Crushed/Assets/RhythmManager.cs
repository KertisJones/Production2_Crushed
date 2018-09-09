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

    public float minDistPlayer = 1f;
    public float minDistFinish = 1f;

    public float timeBetweenBeats = 0.5f;
    public float delayAfterClick = 0.5f;
    public float timeLeft = 0;
    private float delayTimeLeft = 0;
    public float sizeModifier = 0;
    private bool clickedThisBeat = false;
    private int perfectBeats = 0;

    // Use this for initialization
    void Start () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        finishTrans = GameObject.FindGameObjectWithTag("Finish").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            delayTimeLeft -= Time.deltaTime;
            //sizeModifier = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * (180 * (timeBetweenBeats / timeLeft))));
            if (timeLeft >= timeBetweenBeats / 2)
            {
                sizeModifier = 2 * ((timeLeft - (timeBetweenBeats / 2)) / timeBetweenBeats);
            }
            else
            {
                sizeModifier = 2 * (((timeBetweenBeats / 2) - timeLeft) / timeBetweenBeats);
            }


            if (timeLeft > timeBetweenBeats * 0.49 && timeLeft < timeBetweenBeats * 0.51)
            {
                clickedThisBeat = false;
            }
        }
        else
        {
            timeLeft = timeBetweenBeats;
            AudioSource.PlayClipAtPoint(beatSound, new Vector3(0f, 0f, -10f));
        }


        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Trying to spawn...");
            Vector3 spawnPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (Vector3.Distance(spawnPoint, playerTrans.position) >= minDistPlayer && delayTimeLeft < 0)
            {
                Spawn(spawnPoint);
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
        GameObject block = Instantiate(wall, spawnPoint, transform.rotation) as GameObject;
        float newSize = 0.25f + (2.75f * sizeModifier);
        block.transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}
