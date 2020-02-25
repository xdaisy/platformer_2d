using UnityEngine;

/// <summary>
/// Class that handle treat powering up player
/// </summary>
public class Treat : MonoBehaviour {
    [SerializeField] private int score;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // if player collided with, power up player
            Player player = other.gameObject.GetComponent<Player>();
            player.PowerUp();
            Inventory.Instance.IncrementScore(score);

            Destroy(gameObject);
        }
    }
}
