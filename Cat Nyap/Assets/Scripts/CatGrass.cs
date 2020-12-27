using UnityEngine;

/// <summary>
/// Class that handles collecting cat grass
/// </summary>
public class CatGrass : MonoBehaviour {
    [SerializeField] private int score;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // if player collided with, add cat grass to inventory
            Inventory.Instance.AddCatGrass(1);
            Inventory.Instance.IncrementScore(score);

            Destroy(gameObject);
        }
    }
}
