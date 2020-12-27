using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the title screen
/// </summary>
public class MenuSystem : MonoBehaviour {
    public Button[] Buttons;
    public GameObject ControlsScreen;
    public GameObject BackButton;

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

    /// <summary>
    /// Open the Controls screen
    /// </summary>
    public void OpenTutorial() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        EventSystem.current.SetSelectedGameObject(null);
        foreach (Button button in Buttons) {
            button.interactable = false;
        }
        EventSystem.current.SetSelectedGameObject(BackButton);
        ControlsScreen.SetActive(true);
    }

    /// <summary>
    /// Close the Controls screen
    /// </summary>
    public void CloseTutorial() {
        AudioManager.Instance.PlaySFX(Constants.CLICK_SFX);
        EventSystem.current.SetSelectedGameObject(null);
        foreach (Button button in Buttons) {
            button.interactable = true;
        }
        EventSystem.current.SetSelectedGameObject(Buttons[0].gameObject);
        ControlsScreen.SetActive(false);
    }

}
