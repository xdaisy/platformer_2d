using UnityEngine;

public class PlayBGM : MonoBehaviour {
    [SerializeField] int bgm;

    // Start is called before the first frame update
    void Start() {
        AudioManager.Instance.PlayBGM(bgm);
    }
}
