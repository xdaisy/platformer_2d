using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {
    public float Speed = 100f;

    private readonly float height = 1343.8f;

    // Update is called once per frame
    void Update() {
        if (this.transform.localPosition.y >= height) {
            SceneManager.LoadScene(Constants.WIN_SCENE);
        }
        this.transform.position = this.transform.position + Vector3.up * Speed * Time.deltaTime;
    }
}
