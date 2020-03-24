using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that handles what happens when the player gets to the bed
/// </summary>
public class Bed : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        // TODO: animate
        
        SceneManager.LoadScene(Constants.WIN_SCENE);
    }
}
