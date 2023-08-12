using UnityEngine;
using UnityEngine.Assertions;

public class EnemyAttack : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    private void Update() {
        RunCDTimerAndSetAtkFlag();

        if (IsTargetInRange() && canAttack) {
            PerformBasicAttack();

            canAttack = false;
        }
    }

    private void RunCDTimerAndSetAtkFlag() {
        if (!canAttack) {
            cdTimer += Time.deltaTime;

            if (cdTimer >= cooldown) {
                canAttack = true;
                cdTimer = 0f;
            }
        }
    }

    private bool IsTargetInRange() {
        Assert.IsNotNull(target, "EnemyAttack does not have a target");

        return Vector2.Distance(transform.position, target.position) <= attackRange;
    }

    private void PerformBasicAttack() {
        Assert.IsNotNull(weapon, "EnemyAttack is missing a weapon");

        Projectile projectile =
            Instantiate(weapon, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetRotation(GetProjectileRotationFacingTarget());
        projectile.SetVelocity(GetProjectileVelocityFacingTarget());
    }

    private Quaternion GetProjectileRotationFacingTarget() {
        Vector3 rotation = transform.position - target.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rot + 90f);
    }

    private Vector2 GetProjectileVelocityFacingTarget() {
        Vector3 direction = target.position - transform.position;
        return new Vector2(direction.x, direction.y).normalized;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameObject weapon;

    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private float cooldown = 2f;

    private Transform target;
    private float cdTimer = 0f;
    private bool canAttack = true;
    #endregion
}
