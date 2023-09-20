using UnityEngine;
using UnityEngine.Assertions;

public class HeartPickup : Powerup {
    #region Methods
    protected override void Start() {
        base.Start();

        Assert.IsNotNull(onSpawn, "HeartPickup is missing onSpawn event");

        onSpawn.Raise();
    }

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

    [SerializeField]
    private VoidGameEventSO onSpawn;
    #endregion
}
