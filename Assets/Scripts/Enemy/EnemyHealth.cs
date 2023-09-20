using UnityEngine;
using UnityEngine.Assertions;

public class EnemyHealth : Health {
    #region Methods
    public override void TakeDamage(float damage) {
        if (gameObject != enemyHit) {
            return;
        }

        Assert.IsNotNull(onEnemyDeath, "Enemy death game event not assigned in EnemyHealth");

        base.TakeDamage(damage);

        if (currentHealth <= 0) {
            onEnemyDeath.Raise(gameObject);
        }
    }

    public void SetHitEnemyGameobject(GameObject hitEnemy) {
        enemyHit = hitEnemy;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameobjectGameEventSO onEnemyDeath;

    private GameObject enemyHit = null;
    #endregion
}
