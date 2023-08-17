using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class RandomWalkDungeonGenerator : AbstractDungeonGenerator {
    #region Methods
    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int position) {
        Assert.IsNotNull(randomWalkSO, "Scriptable object missing in RandomWalkDungeonGenerator");

        HashSet<Vector2Int> floorPositions = new();
        Vector2Int currentPosition = position;

        for (int i = 0; i < randomWalkSO.iterations; ++i) {
            HashSet<Vector2Int> path =
                ProceduralGenerator.RandomWalk(currentPosition, randomWalkSO.walkLength);

            floorPositions.UnionWith(path);

            if (randomWalkSO.startRandomlyEachIteration) {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }

    protected override void RunProceduralGenerator() {
        Assert.IsNotNull(painter, "TilemapPainter reference not found in RandomWalkGenerator");

        var floorPositions = RunRandomWalk(startPosition);

        painter.PaintFloor(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, painter);
    }

    protected override void RunGeneratorCleanup() {
        Assert.IsNotNull(painter, "TilemapPainter reference not found in RandomWalkGenerator");

        painter.ClearAll();
    }
    #endregion

    #region Member variables
    [SerializeField]
    protected RandomWalkSO randomWalkSO;
    #endregion
}
