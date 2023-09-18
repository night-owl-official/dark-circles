using UnityEngine;

public class HeartPickup : Powerup {
    #region Methods
    protected override void OnTriggerEnter2D(Collider2D other) {
        if (!IsPartOfCollisionMask(other)) return;

        onPickup.Raise(recoverAmount);

        Destroy(gameObject);
    }

    public void EnablePickup(bool shouldEnable) {
        TogglePickup(shouldEnable);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float recoverAmount = 1f;
    #endregion
}
