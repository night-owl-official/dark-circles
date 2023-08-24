using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHealth : Health {
    #region Methods
    public override void TakeDamage(float damage) {
        Assert.IsNotNull(onPlayerDeath, "Player Death game event is missing from PlayerHealth");

        base.TakeDamage(damage);

        if (currentHealth <= 0) {
            onPlayerDeath.Raise();
        }
    }
    #endregion

    #region Member variables
    [SerializeField]
    private VoidGameEventSO onPlayerDeath;
    #endregion
}
