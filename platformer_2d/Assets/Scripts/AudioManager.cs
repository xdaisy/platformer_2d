using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [SerializeField] private AudioClip[] BGM;
    [SerializeField] private AudioClip[] SFX;

    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Play background music
    /// </summary>
    /// <param name="bgm">Index of the BGM to play</param>
    public void PlayBGM(int bgm) {
        if (bgm < BGM.Length && (AudioSource.clip == null || !AudioSource.clip.Equals(BGM[bgm]))) {
            if (AudioSource.isPlaying) {
                // stop the current clip is that being played
                AudioSource.Stop();
            }
            AudioSource.clip = BGM[bgm];
            AudioSource.Play();
        }
    }

    /// <summary>
    /// Play sound effect
    /// </summary>
    /// <param name="sfx">Index of the SFX to play</param>
    public void PlaySFX(int sfx) {
        if (sfx < SFX.Length) {
            AudioSource.PlayOneShot(SFX[sfx]);
        }
    }
}
