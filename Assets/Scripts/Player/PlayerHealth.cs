using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHealth : Health {
    #region Methods
    public override void TakeDamage(float damage) {
        Assert.IsNotNull(onPlayerDeath, "Player Death game event is missing from PlayerHealth");
        Assert.IsNotNull(onHealthDecreased, "Player Health Decrease game event is missing from PlayerHealth");
        Assert.IsNotNull(onToggleHeartPickup, "Toggle Heart pickup game event is missing from PlayerHealth");

        base.TakeDamage(damage);
        onHealthDecreased.Raise(damage);

        onToggleHeartPickup.Raise(true);

        if (currentHealth <= 0) {
            onPlayerDeath.Raise();
        }
    }

    public override void HealUp(float amount) {
        Assert.IsNotNull(onHealthIncreased, "Player Health Increase game event is missing from PlayerHealth");
        Assert.IsNotNull(onToggleHeartPickup, "Toggle Heart pickup game event is missing from PlayerHealth");

        base.HealUp(amount);
        onHealthIncreased.Raise(amount);

        if (currentHealth == maxHealth) {
            onToggleHeartPickup.Raise(false);
        }
    }

    public void ToggleSpawnedHeartPickup() {
        if (currentHealth < maxHealth) {
            onToggleHeartPickup.Raise(true);
        } else {
            onToggleHeartPickup.Raise(false);
        }
    }
    #endregion

    #region Member variables
    [SerializeField]
    private VoidGameEventSO onPlayerDeath;
    [SerializeField]
    private FloatGameEventSO onHealthDecreased;
    [SerializeField]
    private FloatGameEventSO onHealthIncreased;
    [SerializeField]
    private BoolGameEventSO onToggleHeartPickup;
    #endregion
}
