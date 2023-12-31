using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : Spawner {
    #region Methods
    public override GameObject Spawn(Vector3 position) {
        Assert.IsNotNull(spawnee, "Enemy prefab is missing in EnemySpawner");

        return Instantiate(spawnee, position, Quaternion.identity);
    }

    public void SetSpawnee(GameObject enemy) => spawnee = enemy;
    #endregion
}
