using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {
    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame() {
        SceneManager.LoadScene(Constants.STAGE_SCENE);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
