using UnityEngine;

public class CatGrass : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // if player collided with, add cat grass to inventory
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.AddCatGrass(1);

            Destroy(gameObject);
        }
    }
}
