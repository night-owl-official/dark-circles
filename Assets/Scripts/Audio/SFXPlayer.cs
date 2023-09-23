using UnityEngine;
using UnityEngine.EventSystems;

public class SFXPlayer : AudioPlayer, IPointerEnterHandler {
    #region Methods
    public void OnPointerEnter(PointerEventData eventData) {
        if (!audioSource || !menuNavigateClip) { return; }

        PlayClip(menuNavigateClip);
    }

    public void PlayMenuSelectClip() {
        if (!menuSelectClip) { return; }
        PlayClip(menuSelectClip);
    }

    public void PlayEnemyHitClip() {
        if (!enemyHitClip) { return; }
        PlayClip(enemyHitClip);
    }

    public void PlayPlayerHitClip() {
        if (!playerHitClip) { return; }
        PlayClip(playerHitClip);
    }

    public void PlayPowerupCollectClip() {
        if (!powerupCollectClip) { return; }
        PlayClip(powerupCollectClip);
    }

    public void PlayHeartCollectClip() {
        if (!heartCollectClip) { return; }
        PlayClip(heartCollectClip);
    }

    public void PlayCircleClearClip() {
        if (!circleClearClip) { return; }
        PlayClip(circleClearClip);
    }

    public void PlayCircleFailClip() {
        if (!circleFailClip) { return; }
        PlayClip(circleFailClip);
    }
    #endregion

    #region Member variables
    [Header("Menu")]

    [SerializeField]
    private AudioClip menuNavigateClip;
    [SerializeField]
    private AudioClip menuSelectClip;

    [Space(16)]
    [Header("Game")]

    [SerializeField]
    private AudioClip enemyHitClip;
    [SerializeField]
    private AudioClip playerHitClip;
    [SerializeField]
    private AudioClip powerupCollectClip;
    [SerializeField]
    private AudioClip heartCollectClip;

    [SerializeField]
    private AudioClip circleClearClip;
    [SerializeField]
    private AudioClip circleFailClip;
    #endregion
}
