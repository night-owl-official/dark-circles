using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour {
    #region Methods
    public void SetChaseTrigger(bool flag) {
        if (gameObject != enemyTriggered) {
            return;
        }

        chaseTriggered = flag;
    }

    public void SetEnemyTriggered(GameObject enemy) => enemyTriggered = enemy;

    // Start is called before the first frame update
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    private void Update() {
        if (chaseTriggered) {
            ChaseTarget();
        } else {
            TriggerChaseWhenTargetInRadius();
        }
    }

    private void TriggerChaseWhenTargetInRadius() {
        chaseTriggered = Vector2.Distance(transform.position, target.position) < chaseRadius;
    }

    private void ChaseTarget() {
        FaceTarget();
        UpdatePositionBasedOnTarget();
    }

    private void FaceTarget() {
        Assert.IsNotNull(target, "No target found on EnemyMovement");
        Assert.IsNotNull(spriteRenderer, "No sprite renderer found on EnemyMovement");

        spriteRenderer.flipX = target.position.x < transform.position.x;
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

    [SerializeField]
    private float chaseRadius = 5f;

    private SpriteRenderer spriteRenderer;
    private Transform target;
    private bool chaseTriggered = false;
    private GameObject enemyTriggered = null;
    #endregion
}
