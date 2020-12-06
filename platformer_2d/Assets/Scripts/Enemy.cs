using UnityEngine;

/// <summary>
/// Super class for Enemy classes
/// </summary>
public class Enemy : MonoBehaviour {
    [SerializeField] private int moveSpeed; // movement speed of the enemy
    [SerializeField] private int score;
    [SerializeField] private LayerMask enemyLayerMask;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Vector3 hit = other.contacts[0].normal;
            float angle = Vector3.Angle(hit, Vector3.up);
            if (Mathf.Approximately(angle, 180)) {
                // Up
                Player player = other.gameObject.GetComponent<Player>();
                player.ApplyBounce();
                Destroy(this.gameObject);
                Inventory.Instance.IncrementScore(this.score);
            } else {
                // Not Up
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
