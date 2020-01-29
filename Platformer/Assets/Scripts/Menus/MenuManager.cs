using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    private void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Quits the game
    public void Quit() => Application.Quit();

    // loads the main menu
    public void QuitToMainMenu() => StartLevel("MainMenu");

    // restarts the current scene
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    // starts the scene with the given name
    public void StartLevel(string name) => SceneManager.LoadScene(name);
}
