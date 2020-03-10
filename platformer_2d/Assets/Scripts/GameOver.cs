using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for Game Over screen
/// </summary>
public class GameOver : MonoBehaviour {
    /// <summary>
    /// Restart the stage
    /// </summary>
    public void ClickYes() {
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.STAGE_SCENE);
    }

    /// <summary>
    /// Return to main menu scene
    /// </summary>
    public void ClickNo() {
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.MAIN_SCENE);
    }
}
