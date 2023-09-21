using UnityEngine;
using UnityEngine.Assertions;

public class Projectile : MonoBehaviour {
    #region Methods
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Assert.IsNotNull(onHitDamage, "hit game event missing from Projectile");

        if (IsPartOfHitMask(other)) {
            if (onHitGameObject) {
                onHitGameObject.Raise(other.gameObject);
            }

            if (onHitChaseTrigger) {
                onHitChaseTrigger.Raise(true);
            }

            onHitDamage.Raise(hitDamage);
        }

        Destroy(gameObject);
    }

    private bool IsPartOfHitMask(Collider2D collider) {
        return ((1 << collider.gameObject.layer) & hitMask) != 0;
    }

    public void SetRotation(Quaternion projectileRotation) {
        transform.rotation = projectileRotation;
    }

    public void SetVelocity(Vector2 projectileVelocity) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = projectileVelocity * force;
    }

    public void SetHitDamage(float damage) {
        hitDamage = damage;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float force = 5f;

    [SerializeField]
    private FloatGameEventSO onHitDamage;
    [SerializeField]
    private GameobjectGameEventSO onHitGameObject;
    [SerializeField]
    private BoolGameEventSO onHitChaseTrigger;

    [SerializeField]
    private LayerMask hitMask;

    private float hitDamage = 0.5f;
    #endregion
}
