using UnityEngine;

public class Projectile : MonoBehaviour {
    #region Methods
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    public void SetRotation(Quaternion projectileRotation) {
        transform.rotation = projectileRotation;
    }

    public void SetVelocity(Vector2 projectileVelocity) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = projectileVelocity * force;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float force = 5f;
    #endregion
}
