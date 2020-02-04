using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Text numCatGrass;
    [SerializeField]
    private Text score;

    // Update is called once per frame
    void Update() {
        numCatGrass.text = "" + inventory.GetNumCatGrass();
        score.text = "" + inventory.GetScore();
    }
}
