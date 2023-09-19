using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Powerup : MonoBehaviour {
    #region Methods
    protected void Start() {
        hitBox = GetComponent<BoxCollider2D>();

        hitBox.isTrigger = pickupEnabled;
    }

    protected void TogglePickup(bool value) {
        pickupEnabled = value;
        hitBox.isTrigger = pickupEnabled;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (!IsPartOfCollisionMask(other)) {
            return;
        }

        var item = WeightedChanceItemSelector.GetRandomItemFromList(powerupLevels);
        onPickup.Raise(item);

        Destroy(gameObject);
    }

    protected bool IsPartOfCollisionMask(Collider2D collider) {
        return ((1 << collider.gameObject.layer) & collisionMask) != 0;
    }
    #endregion

    #region Member variables
    [SerializeField]
    protected WeightedValue[] powerupLevels;

    [SerializeField]
    protected LayerMask collisionMask;

    [SerializeField]
    protected bool pickupEnabled = true;

    [SerializeField]
    protected FloatGameEventSO onPickup;

    protected Collider2D hitBox = null;
    #endregion
}
