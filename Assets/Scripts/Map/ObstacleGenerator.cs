using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class ObstacleGenerator : MonoBehaviour {
    #region Methods
    public void Place(HashSet<Vector2Int> positions, TilemapPainter painter) {
        Assert.IsTrue(obstacles.Length > 0, "Not enough obstacles assigned to ObstacleGenerator");

        int randomObstaclesAmount = Random.Range(minAmount, maxAmount);
        int currentObstaclesAmount = 0;

        while (currentObstaclesAmount < randomObstaclesAmount) {
            painter.PaintDecal(GetRandomPosition(positions), GetRandomObstacle());

            currentObstaclesAmount++;
        }
    }

    private Vector2Int GetRandomPosition(HashSet<Vector2Int> positions) {
        Vector2Int randomPosition;

        do {
            randomPosition = positions.ElementAt(Random.Range(0, positions.Count));
        } while (usedPositions.Contains(randomPosition));

        usedPositions.Add(randomPosition);

        return randomPosition;
    }

    private TileBase GetRandomObstacle() {
        int randomIndex = Random.Range(0, obstacles.Length);
        return obstacles[randomIndex];
    }

    public HashSet<Vector2Int> GetUsedPositions() {
        return usedPositions;
    }

    public void ClearPositions() {
        usedPositions.Clear();
    }
    #endregion

    #region Member variables
    [SerializeField]
    private int minAmount = 20;
    [SerializeField]
    private int maxAmount = 40;

    [SerializeField]
    private TileBase[] obstacles;

    private HashSet<Vector2Int> usedPositions = new();
    #endregion
}
