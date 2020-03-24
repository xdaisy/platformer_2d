using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for game over
/// </summary>
public class GameOver : MonoBehaviour {
    /// <summary>
    /// Restart the stage
    /// </summary>
    public void PlayAgain() {
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.STAGE_SCENE);
    }

    /// <summary>
    /// Return to main menu scene
    /// </summary>
    public void Quit() {
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.MAIN_SCENE);
    }
}
