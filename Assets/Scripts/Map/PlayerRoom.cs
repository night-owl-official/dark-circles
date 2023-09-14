using System.Collections.Generic;
using UnityEngine;

public class PlayerRoom : Room {
    #region Methods
    public PlayerRoom(Vector2Int startPos,
        HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        Spawner spawner)
        : base(positions, roomData, spawner) {
        startPosition = startPos;
    }

    public void PlacePlayer() {
        PlayerSpawner playerSpawner = (PlayerSpawner) spawner;
        var player = playerSpawner.Spawn((Vector3Int) startPosition);
        spawnedEntities.Add(player);
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
