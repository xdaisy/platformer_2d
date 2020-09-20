using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour {
    public static EndStage Instance;

    private Animator anim;

    private float animationTime = 1.5f;

    // Start is called before the first frame update
    void Start() {
        if (EndStage.Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Play the stage finish animation
    /// </summary>
    /// <param name="isPoweredUp">Flag for whether or not the player was powered up</param>
    public void PlayEndAnimation(bool isPoweredUp) {
        this.gameObject.SetActive(true);

        // play animation
        StartCoroutine(playAnimation(isPoweredUp));
    }

    private IEnumerator playAnimation(bool isPoweredUp) {
        if (isPoweredUp) {
            anim.SetTrigger("Play_Collar");
        } else {
            anim.SetTrigger("Play_No_Collar");
        }

        yield return new WaitForSeconds(animationTime);

        SceneManager.LoadScene(Constants.WIN_SCENE);
    }
}
