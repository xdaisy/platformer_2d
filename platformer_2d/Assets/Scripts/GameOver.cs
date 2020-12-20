using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for game over
/// </summary>
public class GameOver : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Vertical")) {
            AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        }
    }

    /// <summary>
    /// Restart the stage
    /// </summary>
    public void PlayAgain() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.STAGE_SCENE);
    }

    /// <summary>
    /// Return to main menu scene
    /// </summary>
    public void Quit() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.MAIN_SCENE);
    }
}
