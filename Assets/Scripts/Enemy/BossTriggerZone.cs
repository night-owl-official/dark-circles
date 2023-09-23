using UnityEngine;

public class BossTriggerZone : MonoBehaviour {
    #region Methods
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.GetComponent<PlayerMovement>()) {
            return;
        }

        onBossTriggered.Raise();
    }
    #endregion

    #region Member variables
    [SerializeField]
    private VoidGameEventSO onBossTriggered;
    #endregion
}
