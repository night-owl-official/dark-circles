using System.Collections.Generic;
using UnityEngine;

public abstract class Room {
    #region Methods
    public Room(HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        Spawner spawner) {
        this.positions = new(positions);
        this.roomData = roomData;
        this.spawner = spawner;
    }

    protected void AddEntity(GameObject entity) {
        if (spawnedEntities.Contains(entity)) 
            return;

        spawnedEntities.Add(entity);
    }

    public virtual void ClearRoomData() {
        positions.Clear();

        foreach (var entity in spawnedEntities) {
            if (entity != null) {
                DestroyInAnyMode(entity);
            }
        }

        spawnedEntities.Clear();
    }

    public virtual void RemoveEntity(GameObject entity) {
        if (!spawnedEntities.Contains(entity)) {
            return;
        }

        spawnedEntities.Remove(entity);
        DestroyInAnyMode(entity);
    }

    #if UNITY_EDITOR
        private void DestroyInAnyMode(GameObject go) {
            if (Application.isPlaying == false)
                Object.DestroyImmediate(go);
            else
                Object.Destroy(go);
        }
    #else
        private void DestroyInAnyMode(GameObject go) => Object.Destroy(go);
    #endif
    #endregion

    #region Member variables
    protected RoomDataSO roomData;
    protected Spawner spawner;
    protected HashSet<Vector2Int> positions;
    protected List<GameObject> spawnedEntities = new();
    #endregion
}
