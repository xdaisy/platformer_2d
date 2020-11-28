using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStage : MonoBehaviour {
    public static EndStage Instance;

    private Animator anim;

    private float animationTime = 5.5f;

    private bool stageHasFinished = false;

    // Start is called before the first frame update
    void Start() {
        Instance = this;
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

    /// <summary>
    /// Set the flag that stage has been finished
    /// </summary>
    public void FinishedStage() {
        this.stageHasFinished = true;
    }

    /// <summary>
    /// Get whether or not the stage has finished
    /// </summary>
    /// <returns>True if the stage has finished, false otherwise</returns>
    public bool HasStageFinished() {
        return this.stageHasFinished;
    }
}
