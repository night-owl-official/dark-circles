using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
    #region Methods
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    protected void PlayClip(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
    #endregion

    #region Member variables
    protected AudioSource audioSource;
    #endregion
}
