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

    private void PerformBasicAttack() {
        Assert.IsNotNull(weapon, "EnemyAttack is missing a weapon");

        Instantiate(weapon, transform.position, Quaternion.identity);
    }

    private bool IsTargetInRange() {
        Assert.IsNotNull(target, "EnemyAttack does not have a target");

        return Vector2.Distance(transform.position, target.position) <= attackRange;
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
