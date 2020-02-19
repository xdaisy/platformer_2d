using UnityEngine;
using UnityEngine.SceneManagement;

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
        // TODO: Go back to main menu
    }
}
