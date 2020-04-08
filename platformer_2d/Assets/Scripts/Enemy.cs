using UnityEngine;

/// <summary>
/// Super class for Enemy classes
/// </summary>
public class Enemy : MonoBehaviour {
    [SerializeField] private int moveSpeed; // movement speed of the enemy
    [SerializeField] private int score;

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Vector3 thisPos = this.transform.position;
            Vector3 playerPos = other.transform.position;
            float angle = Mathf.Atan2(playerPos.y - thisPos.y, playerPos.x - thisPos.x) * 180 / Mathf.PI;
            if (angle < 0) angle += 360f;

            if (45f < angle && angle < 135f) {
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
