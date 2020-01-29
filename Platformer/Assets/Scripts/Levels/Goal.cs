using UnityEngine.SceneManagement;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public string nextScene;

    private void OnCollisionEnter(Collision collision) {
        SceneManager.LoadScene("Victory");
    }
}
