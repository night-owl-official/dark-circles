using UnityEngine;

public class PlayerStats : MonoBehaviour {
    #region Methods
    public void UpdateAttackPower(float value) {
        attackPower += value;
        onAttackPowerUpdate?.Invoke(attackPower);
    }

    public void UpdateAttackCooldown(float value) {
        attackCooldown += value;
        onAttackCooldownUpdate?.Invoke(attackCooldown);
    }

    public void UpdateMovementSpeed(float value) {
        movementSpeed += value;
        onMovementSpeedUpdate?.Invoke(movementSpeed);
    }
    #endregion

    #region Member Variables
    [Header("Attack")]

    [SerializeField]
    private float attackPower = 0.5f;
    [SerializeField]
    private float attackCooldown = 1f;

    [Space(16)]
    [Header("Movement")]

    [SerializeField]
    private float movementSpeed = 3f;

    [Space(16)]
    [Header("Events")]

    public FloatEvent onAttackPowerUpdate;
    public FloatEvent onAttackCooldownUpdate;
    public FloatEvent onMovementSpeedUpdate;
    #endregion
}
