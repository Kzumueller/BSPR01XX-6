using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Floor : MonoBehaviour
{
    public Transform LastCheckPoint;

    public Text timeLabel;

    public Text livesLabel;

    public float timeForCompletion = 60f;

    public int startingLives = 5;

    private AudioSource playerAudio;

    private int lives;

    private Timer timer;

    private void Start() {
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        lives = startingLives;
        timer = new Timer(timeForCompletion);
    }

    private void FixedUpdate() {
        if (timer.Finished(Time.deltaTime))
            GameOver();
        else
            timeLabel.text = String.Format("Time: {0:0.0}", timer.time);
    }

    // moves the player to the last checkpoint, i.e. the last platform touched
    // short iteration times, baby!
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;

        AlterLives(-1);

        if (0 < lives)
            collision.gameObject.transform.position = LastCheckPoint.position + new Vector3(0f, .25f);
    }

    // adds/subtracts lives and updates the corresponding label
    public void AlterLives(int addedLives) {
        lives += addedLives;

        if (1 > lives)
            GameOver();

        playerAudio.Play();
        livesLabel.text = $"Lives: {lives}";
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
}
