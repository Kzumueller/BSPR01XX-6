using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Vector3 startPosition;

    private Vector3 lastPosition;

    private Vector3 nextPosition;

    private float startTime;

    private float journeyLength;

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

    // subtracts a life on collision, starts its particle system and destroys itself after that has ended
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;

        FindObjectOfType<Floor>().AlterLives(-1);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        var particles = GetComponent<ParticleSystem>();
        particles.Play();
        Destroy(gameObject, particles.main.duration);
    }

    // Moving itself and the player standing on it to the next position.
    void FixedUpdate() {
        // Distance moved equals elapsed time times speed..
        var distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        var fractionOfJourney = distCovered / journeyLength;

        var oldPosition = transform.position;

        // Sets our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(lastPosition, nextPosition, fractionOfJourney);

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
