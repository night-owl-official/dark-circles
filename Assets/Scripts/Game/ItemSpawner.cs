using UnityEngine;
using UnityEngine.Assertions;

public class ItemSpawner : Spawner {
    public override GameObject Spawn(Vector3 position) {
        Assert.IsNotNull(spawnee, "Item prefab is missing in ItemSpawner");

        return Instantiate(spawnee, position, Quaternion.identity);
    }

    public void SetSpawnee(GameObject item) => spawnee = item;
}
