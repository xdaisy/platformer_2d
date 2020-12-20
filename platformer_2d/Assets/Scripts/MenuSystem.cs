using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the title screen
/// </summary>
public class MenuSystem : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Horizontal")) {
            AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        }
    }

    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        SceneManager.LoadScene(Constants.STAGE_SCENE);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        Application.Quit();
    }
}
