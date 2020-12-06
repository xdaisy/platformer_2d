using UnityEngine;

/// <summary>
/// Class that handles what happens when the player gets to the bed
/// </summary>
public class Bed : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Inventory.Instance.IncrementScore(Constants.FINISH_STAGE_POINTS);
            EndStage.Instance.FinishedStage();
        }
    }
}
