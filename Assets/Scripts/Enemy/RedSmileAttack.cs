using System.Collections;
using UnityEngine;

public class RedSmileAttack : MonoBehaviour {
    #region Methods
    public void TriggerBoss() {
        target = FindObjectOfType<PlayerMovement>().gameObject;
        StartCoroutine(MoveRoutine());
    }

    public void Die(GameObject enemyDead) {
        if (gameObject != enemyDead) {
            return;
        }

        StopAllCoroutines();
        animator.SetBool(runAnimParamName, false);
        animator.SetTrigger(deadAnimParamName);
    }

    public void RaiseDeathEvent() {
        onBossCompletedDeathAnimation.Raise(gameObject);
    }

    private IEnumerator MoveRoutine() {
        while (true) {
            if (!isMoving) {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                Vector2 targetPosition = (Vector2) transform.position + randomDirection * moveSpeed;

                isMoving = true;
                animator.SetBool(runAnimParamName, true);

                FlipSpriteBasedOnMoveDirection(targetPosition);

                if (Random.value < 0.3f) {
                    targetPosition = target.transform.position;
                }

                StartCoroutine(MoveToTarget(targetPosition));
            }

            yield return null;
        }
    }

    private void FlipSpriteBasedOnMoveDirection(Vector2 targetPosition) {
        if ((targetPosition - (Vector2) transform.position).x > 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    private IEnumerator MoveToTarget(Vector2 targetPosition) {
        if (Random.value < 0.1f) {
            TeleportNextToTarget();
        } else {
            while ((Vector2) transform.position != targetPosition) {
                transform.position =
                    Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                yield return null;
            }
        }

        animator.SetBool(runAnimParamName, false);

        waitTimer = Random.Range(minWait, maxWait);
        yield return new WaitForSeconds(waitTimer);

        isMoving = false;
    }

    private void TeleportNextToTarget() {
        float randomValue = Random.Range(2f, 5f);
        Vector2 randomOffset = new(randomValue, randomValue);
        transform.position = (Vector2) target.transform.position + randomOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.collider.GetComponent<PlayerMovement>()) {
            return;
        }

        onBossBumpedPlayer.Raise(damage);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private FloatGameEventSO onBossBumpedPlayer;
    [SerializeField]
    private GameobjectGameEventSO onBossCompletedDeathAnimation;

    [Header("Animation")]

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private string runAnimParamName = "isRunning";
    [SerializeField]
    private string deadAnimParamName = "isDead";

    [Header("Movement")]

    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float minWait = 0.5f;
    [SerializeField]
    private float maxWait = 3f;

    [Header("Attack")]

    [SerializeField]
    private float damage = .5f;

    private float waitTimer = 0f;
    private bool isMoving = false;
    private GameObject target;
    #endregion
}
