using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class RandomWalkDungeonGenerator : AbstractDungeonGenerator {
    #region Methods
    private HashSet<Vector2Int> RunRandomWalk() {
        HashSet<Vector2Int> floorPositions = new();
        Vector2Int currentPosition = startPosition;

        for (int i = 0; i < iterations; ++i) {
            HashSet<Vector2Int> path =
                ProceduralGenerator.RandomWalk(currentPosition, walkLength);

            floorPositions.UnionWith(path);

            if (startRandomlyEachIteration) {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }

    protected override void RunProceduralGenerator() {
        Assert.IsNotNull(painter, "TilemapPainter reference not found in RandomWalkGenerator");

        painter.PaintFloor(RunRandomWalk());
    }
    #endregion

    #region Member variables
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    private int walkLength = 10;
    [SerializeField]
    private bool startRandomlyEachIteration = true;
    #endregion
}
