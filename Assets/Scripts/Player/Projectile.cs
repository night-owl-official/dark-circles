using UnityEngine;
using UnityEngine.Assertions;

public class Projectile : MonoBehaviour {
    #region Methods
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Assert.IsNotNull(onHit, "hit game event missing from Projectile");

        if (IsPartOfHitMask(other)) {
            onHit.Raise(hitDamage);
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
    private FloatGameEventSO onHit;

    [SerializeField]
    private LayerMask hitMask;

    private float hitDamage = 0.5f;
    #endregion
}
