using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room {
    #region Methods
    public BossRoom(Vector2Int startPos,
        HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        Spawner spawner)
        : base(positions, roomData, spawner) {
        startPosition = startPos;
    }

    public override void ClearRoomData() {
        base.ClearRoomData();
        startPosition = Vector2Int.zero;
    }

    public void PlaceBoss() {
        EnemySpawner enemySpawner = (EnemySpawner) spawner;
        enemySpawner.SetSpawnee(GetRandomBoss());
        var boss = enemySpawner.Spawn((Vector3Int) startPosition);
        spawnedEntities.Add(boss);
    }

    private GameObject GetRandomBoss() {
        int randomIndex = Random.Range(0, roomData.possibleEntities.Length);
        return roomData.possibleEntities[randomIndex];
    }
    #endregion

    #region Member variables
    private Vector2Int startPosition;
    #endregion
}
