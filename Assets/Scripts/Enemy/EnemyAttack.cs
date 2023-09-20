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
            InitiateAttack();
            canAttack = false;
        }
    }

    private void RunCDTimerAndSetAtkFlag() {
        if (!canAttack) {
            cdTimer += Time.deltaTime;

            float randomCooldown = Random.Range(minCooldown, maxCooldown);

            if (cdTimer >= randomCooldown) {
                canAttack = true;
                cdTimer = 0f;
            }
        }
    }

    private bool IsTargetInRange() {
        Assert.IsNotNull(target, "EnemyAttack does not have a target");

        return Vector2.Distance(transform.position, target.position) <= attackRange;
    }

    private void InitiateAttack() {
        if (canUseAnyAttack) {
            if (Random.value < 0.5f) {
                PerformBasicAttack();
            } else {
                PerformRadiusAttack();
            }
        } else if (shouldUseRadiusAttack) {
            PerformRadiusAttack();
        } else {
            PerformBasicAttack();
        }
    }

    private void PerformBasicAttack() {
        Assert.IsNotNull(weapon, "EnemyAttack is missing a weapon");

        Projectile projectile =
            Instantiate(weapon, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetVelocity(GetProjectileVelocityFacingTarget());
    }

    private void PerformRadiusAttack() {
        int randomNumBullets = Random.Range(minProjectiles, maxProjectiles + 1);

        for (int i = 0; i < randomNumBullets; i++) {
            float angle = i * (360f /  randomNumBullets);
            Vector3 spawnPosition =
                transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * shootingRadius;

            Projectile projectile =
                Instantiate(weapon, spawnPosition, Quaternion.identity).GetComponent<Projectile>();
            projectile.SetVelocity(Quaternion.Euler(0, 0, angle) * Vector2.right);
        }
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
    private int minProjectiles = 1;

    [SerializeField]
    private int maxProjectiles = 5;

    [SerializeField]
    private float shootingRadius = 3f;

    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private float minCooldown = .5f;
    [SerializeField]
    private float maxCooldown = 3f;

    [SerializeField]
    private bool shouldUseRadiusAttack = false;
    [SerializeField]
    private bool canUseAnyAttack = false;

    private Transform target;
    private float cdTimer = 0f;
    private bool canAttack = true;
    #endregion
}
