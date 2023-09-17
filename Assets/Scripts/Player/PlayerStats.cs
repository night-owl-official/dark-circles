using UnityEngine;

public class PlayerStats : MonoBehaviour {
    #region Methods
    private void Start() {
        ResetStats();
    }

    private void ResetStats() {
        currentAttackPower = baseAttackPower;
        currentAttackCooldown = baseAttackCooldown;
        currentMovementSpeed = baseMovementSpeed;

        onAttackPowerUpdate?.Invoke(currentAttackPower);
        onAttackCooldownUpdate?.Invoke(currentAttackCooldown);
        onMovementSpeedUpdate?.Invoke(currentMovementSpeed);
    }

    public void UpdateAttackPower(float value) {
        currentAttackPower += value;
        onAttackPowerUpdate?.Invoke(currentAttackPower);
    }

    public void UpdateAttackCooldown(float value) {
        currentAttackCooldown += value;
        onAttackCooldownUpdate?.Invoke(currentAttackCooldown);
    }

    public void UpdateMovementSpeed(float value) {
        currentMovementSpeed += value;
        onMovementSpeedUpdate?.Invoke(currentMovementSpeed);
    }
    #endregion

    #region Member Variables
    [Header("Attack")]

    [SerializeField]
    private float baseAttackPower = 0.5f;
    [SerializeField]
    private float baseAttackCooldown = 1f;

    [Space(16)]
    [Header("Movement")]

    [SerializeField]
    private float baseMovementSpeed = 3f;

    [Space(16)]
    [Header("Events")]

    public FloatEvent onAttackPowerUpdate;
    public FloatEvent onAttackCooldownUpdate;
    public FloatEvent onMovementSpeedUpdate;

    private float currentAttackPower;
    private float currentAttackCooldown;
    private float currentMovementSpeed;
    #endregion
}
