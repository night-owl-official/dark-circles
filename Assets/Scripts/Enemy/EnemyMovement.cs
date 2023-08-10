using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    private void Update() {
        ChaseTarget();
    }

    private void ChaseTarget() {
        FaceTarget();
        UpdatePositionBasedOnTarget();
    }

    private void FaceTarget() {
        Assert.IsNotNull(target, "No target found on EnemyMovement");
        Assert.IsNotNull(spriteRenderer, "No sprite renderer found on EnemyMovement");

        if (target.position.x > transform.position.x) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }

    private void UpdatePositionBasedOnTarget() {
        Assert.IsNotNull(target, "No target found on EnemyMovement");

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * directionToTarget;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private float speed = 1.0f;

    private SpriteRenderer spriteRenderer;
    private Transform target;
    #endregion
}
