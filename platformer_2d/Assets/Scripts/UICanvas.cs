using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {
    [SerializeField] private Text numCatGrass;
    [SerializeField] private Text score;
    [SerializeField] private Text lives;

    // Update is called once per frame
    void Update() {
        numCatGrass.text = "" + Inventory.Instance.GetNumCatGrass();
        score.text = "" + Inventory.Instance.GetScore();
        lives.text = "" + Inventory.Instance.GetNumLives();
    }
}
