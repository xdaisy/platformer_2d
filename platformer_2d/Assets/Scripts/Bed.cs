using UnityEngine;

/// <summary>
/// Class that handles what happens when the player gets to the bed
/// </summary>
public class Bed : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        // TODO: animate

        Player player = other.gameObject.GetComponent<Player>();
        player.FinishedStage();
    }
}
