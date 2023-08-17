using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstDungeonGenerator : RandomWalkDungeonGenerator {
    #region Methods
    protected override void RunProceduralGenerator() {
        var floorPositions = RunCorridorRandomWalk();

        painter.PaintFloor(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, painter);
    }

    private HashSet<Vector2Int> RunCorridorRandomWalk() {
        HashSet<Vector2Int> floorPositions = new();

        var currentPosition = startPosition;
        for (int i = 0; i < corridorCount; ++i) {
            var corridor = ProceduralGenerator.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];

            floorPositions.UnionWith(corridor);
        }

        return floorPositions;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private int corridorLength = 14;
    [SerializeField]
    private int corridorCount = 5;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;

    [SerializeField]
    private RandomWalkSO roomGenerationParameters;
    #endregion
}
