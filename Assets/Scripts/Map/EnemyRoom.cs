using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRoom : Room {
    #region Methods
    public EnemyRoom(HashSet<Vector2Int> positions,
        RoomDataSO roomData,
        Spawner spawner) :
        base(positions, roomData, spawner) {}

    public void PlaceEnemies() {
        EnemySpawner enemySpawner = (EnemySpawner) spawner;
        int randomEnemyAmount = Random.Range(roomData.minEntities, roomData.maxEntities + 1);

        for (int i = 0; i < randomEnemyAmount; ++i) {
            enemySpawner.SetSpawnee(GetRandomEnemy());
            var enemy = enemySpawner.Spawn((Vector3Int) GetRandomValidPosition());
            spawnedEntities.Add(enemy);
        }
    }

    private GameObject GetRandomEnemy() {
        int randomIndex = Random.Range(0, roomData.possibleEntities.Length);
        return roomData.possibleEntities[randomIndex];
    }

    private Vector2Int GetRandomValidPosition() {
        Vector2Int validPosition = Vector2Int.zero;

        List<Vector2Int> shuffledPositions = positions.OrderBy(p => Guid.NewGuid()).ToList();

        foreach (var position in shuffledPositions) {
            if (!positions.Contains(position))
                continue;

            int availableNeighborCount = 0;

            foreach (var direction in Direction2D.fullDirections) {
                if (shuffledPositions.Contains(position + direction)) {
                    availableNeighborCount++;
                }
            }

            if (availableNeighborCount >= 5) {
                validPosition = position;
                break;
            }

            positions.Remove(position);
        }

        positions.Remove(validPosition);

        return validPosition;
    }
    #endregion

    #region Member variables

    #endregion
}
