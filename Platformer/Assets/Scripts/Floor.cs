using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform LastCheckPoint;

    // moves the player to the last checkpoint, i.e. the last platform touched
    // short iteration times, baby!
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;

        collision.gameObject.transform.position = LastCheckPoint.position + new Vector3(0f, .25f);
    }
}
