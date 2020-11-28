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
            BoxCollider2D otherCollider = other.gameObject.GetComponent<BoxCollider2D>();
            RaycastHit2D rayCast2d = Physics2D.BoxCast(otherCollider.bounds.center, otherCollider.bounds.size, 0f, Vector2.down, 0.1f, enemyLayerMask);
            if (rayCast2d) {
                // player is on top of enemy
                Player player = other.gameObject.GetComponent<Player>();
                player.ApplyBounce();
                Destroy(this.gameObject);
                Inventory.Instance.IncrementScore(this.score);
            } else {
                // player is not on top of enemy
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
