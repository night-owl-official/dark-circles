using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class Treasure {
    [Range(0f, 100f)]
    public float chance;

    public GameObject treasure;

    [HideInInspector]
    public float weight;
}

public class TreasureRoom : Room {
    #region Methods
    public TreasureRoom(Vector2Int startPos,
        HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        Treasure[] treasures,
        Spawner spawner) : base(positions, roomData, spawner) {
        startPosition = startPos;
        this.treasures = treasures;
    }

    public void PlaceTreasure() {
        ItemSpawner itemSpawner = (ItemSpawner) spawner;
        itemSpawner.SetSpawnee(GetRandomTreasure());
        var treasure = itemSpawner.Spawn((Vector3Int) startPosition);
        spawnedEntities.Add(treasure);
    }

    private GameObject GetRandomTreasure() {
        Assert.IsTrue(treasures.Length > 0, "There are no entries for the chance table of Treasures");

        float randomValue = Random.value * GetAccumulatedWeight();
        foreach (var treasure in treasures) {
            if (randomValue <= treasure.weight) {
                return treasure.treasure;
            }
        }

        return null;
    }

    private float GetAccumulatedWeight() {
        float accumulatedWeight = 0;
        foreach (var treasure in treasures) {
            accumulatedWeight += treasure.chance;
            treasure.weight = accumulatedWeight;
        }
        return accumulatedWeight;
    }

    public override void ClearRoomData() {
        base.ClearRoomData();
        startPosition = Vector2Int.zero;
    }
    #endregion

    #region Member variables
    private Vector2Int startPosition;
    private Treasure[] treasures;
    #endregion
}
