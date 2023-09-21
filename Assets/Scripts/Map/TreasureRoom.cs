using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : Room {
    #region Methods
    public TreasureRoom(Vector2Int startPos,
        HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        WeightedGameObject[] treasures,
        Spawner spawner) : base(positions, roomData, spawner) {
        startPosition = startPos;
        this.treasures = treasures;
    }

    public void PlaceTreasure() {
        ItemSpawner itemSpawner = (ItemSpawner) spawner;
        var item = WeightedChanceItemSelector.GetRandomItemFromList(treasures);
        itemSpawner.SetSpawnee(item);
        var treasure = itemSpawner.Spawn((Vector3Int) startPosition);
        spawnedEntities.Add(treasure);
    }

    public override void RemoveEntity(GameObject entity) {
        if (!spawnedEntities.Contains(entity)) {
            return;
        }

        spawnedEntities.Remove(entity);
    }

    public override void ClearRoomData() {
        base.ClearRoomData();
        startPosition = Vector2Int.zero;
    }
    #endregion

    #region Member variables
    private Vector2Int startPosition;
    private WeightedGameObject[] treasures;
    #endregion
}
