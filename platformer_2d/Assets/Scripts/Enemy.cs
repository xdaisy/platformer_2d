using UnityEngine;

/// <summary>
/// Super class for Enemy classes
/// </summary>
public class Enemy : MonoBehaviour {
    [SerializeField] private int moveSpeed; // movement speed of the enemy
    [SerializeField] private int score;

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Vector2 thisPos = this.transform.position;
            Vector2 playerPos = other.transform.position;
            float angle = Vector2.Angle(playerPos, thisPos);

            if (angle < 4f) {
                // enemy dies
                Destroy(this.gameObject);
                Inventory.Instance.IncrementScore(this.score);
            } else {
                Player player = other.gameObject.GetComponent<Player>();
                player.Hit();
            }
        }
    }

    /// <summary>
    /// Get the enemy's movement speed
    /// </summary>
    /// <returns>Enemy's movement speed</returns>
    public int GetMoveSpeed() {
        return this.moveSpeed;
    }

    /// <summary>
    /// Get the enemy's score
    /// </summary>
    /// <returns>Enemy's score</returns>
    public int GetScore() {
        return this.score;
    }
}
