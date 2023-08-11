using UnityEngine;

public class Projectile : MonoBehaviour {
    #region Methods
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    private void Start() {
        SetTarget();
        SetRotation();
        SetVelocity();
    }

    private void SetTarget() {
        if (aimAtMouse) {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return;
        }

        // Target's the player
        targetPosition = FindObjectOfType<PlayerMovement>().transform.position;
    }

    private void SetRotation() {
        Vector3 rotation = transform.position - targetPosition;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90f);
    }

    private void SetVelocity() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 direction = targetPosition - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private bool aimAtMouse = false;

    [SerializeField]
    private float force = 5f;

    private Vector3 targetPosition;
    #endregion
}
