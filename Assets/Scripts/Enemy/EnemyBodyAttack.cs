using UnityEngine;
using UnityEngine.Assertions;

public class EnemyBodyAttack : MonoBehaviour {
    #region Methods
    private void OnCollisionEnter2D(Collision2D collision) {
        Assert.IsNotNull(onBodyContact, "No body contact event given to EnemyBodyAttack");

        if (!IsPartOfHitMask(collision.collider)) {
            return;
        }

        onBodyContact.Raise(damage);
    }

    private bool IsPartOfHitMask(Collider2D collider) {
        return ((1 << collider.gameObject.layer) & hitMask) != 0;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float damage = 0.5f;

    [SerializeField]
    private LayerMask hitMask;

    [SerializeField]
    private FloatGameEventSO onBodyContact;
    #endregion
}
