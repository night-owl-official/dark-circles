using UnityEngine;
using UnityEngine.Assertions;

public class PickupDropper : MonoBehaviour {
    #region Methods
    private void Start() {
        spawner = FindObjectOfType<ItemSpawner>();
    }

    public void DropPickup(GameObject dropper) {
        if (gameObject != dropper) {
            return;
        }

        if (Random.value * 100f >= dropChance) {
            return;
        }

        Assert.IsNotNull(spawner, "No ItemSpawner given to PickupDropper");
        Assert.IsTrue(drops.Length > 0, "No items to drop assigned to PickupDropper");

        spawner.SetSpawnee(WeightedChanceItemSelector.GetRandomItemFromList(drops));
        spawner.Spawn(dropper.transform.position);
    }
    #endregion

    #region Member variables
    [SerializeField]
    [Range(0f, 100f)]
    private float dropChance = 100f;

    [SerializeField]
    private WeightedGameObject[] drops;

    private ItemSpawner spawner;
    #endregion
}
