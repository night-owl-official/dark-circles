using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : Room {
    #region Methods
    public TreasureRoom(Vector2Int startPos,
        HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        Spawner spawner) : base(positions, roomData, spawner) {
        startPosition = startPos;
    }

    public void PlaceTreasure() {
        ItemSpawner itemSpawner = (ItemSpawner) spawner;
        var treasure = itemSpawner.Spawn((Vector3Int) startPosition);
        spawnedEntities.Add(treasure);
    }

    public override void ClearRoomData() {
        base.ClearRoomData();
        startPosition = Vector2Int.zero;
    }
    #endregion

    #region Member variables
    private Vector2Int startPosition;
    #endregion
}
