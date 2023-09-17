using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAttack : MonoBehaviour {
    #region Methods
    public void SetAttackPower(float value) {
        power = value;
    }

    public void SetAttackCooldown(float value) {
        cooldown = value;
    }

    // Update is called once per frame
    private void Update() {
        RunCDTimerAndSetAtkFlag();
        AttackOnInputAfterCD();
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

    private void AttackOnInputAfterCD() {
        if (!CanPlayerAttack()) return;

        // Stop movement if the player was moving
        onInitiateAttack?.Invoke(false);

        Vector3 mouseViewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        int animationAttackDir = GetAnimationAttackDirection(mouseViewportPosition);
        // Attack animation set
        onPlayerAttack?.Invoke(true);
        onPlayerAttackDirection?.Invoke(animationAttackDir);

        canAttack = false;
    }

    private bool CanPlayerAttack() {
        return Input.GetButtonDown(attackBtnName) && canAttack;
    }

    private int GetAnimationAttackDirection(Vector3 mousePos) {
        if (IsPlayerAttackingUp(mousePos)) {
            // Attacking upwards
            return 0;
        }

        if (IsPlayerAttackingDown(mousePos)) {
            // Attacking downwards
            return 2;
        }

        if (IsPlayerAttackingRight(mousePos) || IsPlayerAttackingLeft(mousePos)) {
            // Attacking sideways
            return 1;
        }

        // Shouldn't get here
        return -1;
    }

    private bool IsPlayerAttackingUp(Vector3 mouseViewportPos) {
        return mouseViewportPos.y > 0.5f &&
            mouseViewportPos.x >= 0.35f && mouseViewportPos.x <= 0.65f;
    }

    private bool IsPlayerAttackingDown(Vector3 mouseViewportPos) {
        return mouseViewportPos.y < 0.5f &&
            mouseViewportPos.x >= 0.35f && mouseViewportPos.x <= 0.65f;
    }

    private bool IsPlayerAttackingRight(Vector3 mouseViewportPos) {
        bool attackingRight = mouseViewportPos.x > 0.5f;

        if (attackingRight) {
            onPlayerSideAttack?.Invoke(false);
        }

        return attackingRight;
    }

    private bool IsPlayerAttackingLeft(Vector3 mouseViewportPos) {
        bool attackingLeft = mouseViewportPos.x < 0.5f;

        if (attackingLeft) {
            onPlayerSideAttack?.Invoke(true);
        }

        return attackingLeft;
    }

    public void InstantiateWeapon() {
        Assert.IsNotNull(playerWeapon, "Player is missing a weapon");

        Projectile projectile =
            Instantiate(playerWeapon, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetRotation(GetProjectileRotationFacingMouse());
        projectile.SetVelocity(GetProjectileVelocityFacingMouse());
        projectile.SetHitDamage(power);
    }

    private Quaternion GetProjectileRotationFacingMouse() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = transform.position - mouseWorldPos;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rot + 90f);
    }

    private Vector2 GetProjectileVelocityFacingMouse() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mouseWorldPos - transform.position;
        return new Vector2(direction.x, direction.y).normalized;
    }
    #endregion

    #region Member variables
    [Header("Input")]

    [SerializeField]
    private string attackBtnName = "Attack";

    [Header("Attack")]

    [SerializeField]
    private GameObject playerWeapon = null;

    public BoolEvent onPlayerAttack;
    public IntEvent onPlayerAttackDirection;
    public BoolEvent onPlayerSideAttack;
    public BoolEvent onInitiateAttack;

    private float cdTimer = 0f;
    private bool canAttack = true;

    private float cooldown;
    private float power;
    #endregion
}
