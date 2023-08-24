using UnityEngine;
using UnityEngine.Assertions;

public class EnemyHealth : Health {
    #region Methods
    public override void TakeDamage(float damage) {
        Assert.IsNotNull(onEnemyDeath, "Enemy death game event not assigned in EnemyHealth");

        base.TakeDamage(damage);

        if (currentHealth <= 0) {
            onEnemyDeath.Raise(gameObject);
        }
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameobjectGameEventSO onEnemyDeath;
    #endregion
}
