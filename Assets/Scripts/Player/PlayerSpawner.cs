using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSpawner : Spawner {
    #region Methods
    public override void Spawn(Vector3 position) {
        Assert.IsNotNull(spawnee, "Player prefab is missing in PlayerSpawner");
        Assert.IsNotNull(onPlayerSpawned, "Spawn event is missing in PlayerSpawner");

        var player = Instantiate(spawnee, position, Quaternion.identity);
        onPlayerSpawned.Raise(player);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private GameobjectGameEventSO onPlayerSpawned;
    #endregion
}
