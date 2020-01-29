using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IPlayerReference
{
    private Vector3 startPosition;

    private Vector3 lastPosition;

    private Vector3 nextPosition;

    private float startTime;

    private float journeyLength;

    private Rigidbody player = null;

    public Vector3 endPosition;

    public float speed = 5f;

    void Start() {
        startPosition = transform.position;
        lastPosition = startPosition;
        nextPosition = endPosition;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    public void UnsetReference() {
        player = null;
    }

    // has every platform unset their player reference, then sets its own
    // this way, the last platform the player touched continues updating their position while airborne,
    // thus seemingly conserving momentum 
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;
            
        FindObjectsOfType<MovingPlatform>().ToList().ForEach(platform => platform.UnsetReference());
        player = collision.gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collision)
    {
        UnsetReference();
    }

    // Moving itself and the player standing on it to the next position.
    void FixedUpdate() {
        // Distance moved equals elapsed time times speed..
        var distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        var fractionOfJourney = distCovered / journeyLength;

        var oldPosition = transform.position;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(lastPosition, nextPosition, fractionOfJourney);

        if (null != player) {
            player.transform.position += transform.position - oldPosition;
        }

        if (.01f > Vector3.Distance(transform.position, endPosition)) {
            nextPosition = startPosition;
            lastPosition = endPosition;
            startTime = Time.time;
        }

        if (.01f > Vector3.Distance(transform.position, startPosition)) {
            nextPosition = endPosition;
            lastPosition = startPosition;
            startTime = Time.time;
        }
    }
}
