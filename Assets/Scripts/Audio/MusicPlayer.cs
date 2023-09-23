using UnityEngine;

public class MusicPlayer : AudioPlayer {
    #region Methods
    private void Start() {
        audioSource.playOnAwake = true;
        audioSource.loop = true;
    }

    public void PlayMenuMusic() {
        if (!menuMusic) {
            return;
        }

        PlayClip(menuMusic);
    }

    public void PlayLimboMusic() {
        if (!limboMusic) { 
            return;
        }

        PlayClip(limboMusic);
    }

    public void PlayLimboBossMusic() {
        if (!limboBossMusic) {
            return;
        }

        PlayClip(limboBossMusic);
    }
    #endregion

    #region Member variables
    [Header("Menu")]

    [SerializeField]
    private AudioClip menuMusic;

    [Space(16)]
    [Header("Limbo")]

    [SerializeField]
    private AudioClip limboMusic;
    [SerializeField]
    private AudioClip limboBossMusic;
    #endregion
}
