using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public bool bouncy = false;
    public float bounciness = 2f;

    // has every moving platform unset their player reference
    // this way, the last platform the player touched continues updating their position while airborne,
    // thus seemingly conserving momentum
    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) return;

        FindObjectsOfType<MovingPlatform>().ToList().ForEach(platform => platform.UnsetReference());
        GameObject.FindGameObjectWithTag("Floor").GetComponent<Floor>().LastCheckPoint = transform;

        if (bouncy)
            collision.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0f, 5f * bounciness);
    }
}
