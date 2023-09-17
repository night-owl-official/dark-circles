using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class PowerupLevel {
    [Range(0f, 100f)]
    public float chance;

    [HideInInspector]
    public float weight;

    public float level;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Powerup : MonoBehaviour {
    #region Methods
    protected void Start() {
        hitBox = GetComponent<BoxCollider2D>();

        hitBox.isTrigger = !isPickupBlocked;
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (!IsPartOfCollisionMask(other)) {
            return;
        }

        onPickup.Raise(GetRandomPowerupLevel());

        Destroy(gameObject);
    }

    private bool IsPartOfCollisionMask(Collider2D collider) {
        return ((1 << collider.gameObject.layer) & collisionMask) != 0;
    }

    protected float GetRandomPowerupLevel() {
        Assert.IsTrue(powerupLevels.Length > 0, "There are no entries for the chance table of Powerup");

        float randomValue = Random.value * GetAccumulatedWeight();
        foreach (var level in powerupLevels) {
            if (randomValue <= level.weight) {
                return level.level;
            }
        }

        return float.NaN;
    }

    private float GetAccumulatedWeight() {
        float accumulatedWeight = 0;
        foreach (var level in powerupLevels) {
            accumulatedWeight += level.chance;
            level.weight = accumulatedWeight;
        }
        return accumulatedWeight;
    }
    #endregion

    #region Member variables
    [SerializeField]
    protected PowerupLevel[] powerupLevels;

    [SerializeField]
    protected LayerMask collisionMask;

    [SerializeField]
    protected bool isPickupBlocked = false;

    [SerializeField]
    protected FloatGameEventSO onPickup;

    private Collider2D hitBox = null;
    #endregion
}
