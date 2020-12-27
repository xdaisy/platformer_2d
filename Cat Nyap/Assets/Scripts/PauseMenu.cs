using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {
    public GameObject YesButton;

    public void QuitGame() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        Inventory.Instance.ResetInventory();
        SceneManager.LoadScene(Constants.MAIN_SCENE);
        Time.timeScale = 1;
    }

    public void OpenMenu() {
        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
        this.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(YesButton);
    }

    public void CloseMenu() {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public bool IsOpen() {
        return this.gameObject.activeSelf;
    }
}
