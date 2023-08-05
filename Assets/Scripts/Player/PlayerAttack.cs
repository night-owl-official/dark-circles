using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAttack : MonoBehaviour {
    #region Methods
    // Update is called once per frame
    private void Update() {
        AttackOnInput();
    }

    private void AttackOnInput() {
        if (!HasPlayerAttacked()) return;

        // Stop movement if the player was moving
        onInitiateAttack?.Invoke(false);

        Vector3 mouseViewportPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        int animationAttackDir = GetAnimationAttackDirection(mouseViewportPosition);
        // Attack animation set
        onPlayerAttack?.Invoke(true);
        onPlayerAttackDirection?.Invoke(animationAttackDir);
    }

    private bool HasPlayerAttacked() {
        return Input.GetButtonDown(attackBtnName);
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

        Instantiate(playerWeapon, transform.position, Quaternion.identity);
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
    #endregion
}
