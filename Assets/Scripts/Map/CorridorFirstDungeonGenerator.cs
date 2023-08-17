using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : RandomWalkDungeonGenerator {
    #region Methods
    protected override void RunProceduralGenerator() {
        RunCorridorFirstDungeonGeneration();
    }

    private void RunCorridorFirstDungeonGeneration() {
        HashSet<Vector2Int> floorPositions = new();
        HashSet<Vector2Int> potentialRoomPositions = new();
        HashSet<Vector2Int> roomPositions = new();

        RunCorridorRandomWalk(floorPositions, potentialRoomPositions);
        RunRoomRandomWalk(roomPositions, potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

        painter.PaintFloor(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, painter);
    }

    private void RunCorridorRandomWalk(HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; ++i) {
            var corridor = ProceduralGenerator.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[^1];

            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }

    private void RunRoomRandomWalk(HashSet<Vector2Int> roomPositions,
        HashSet<Vector2Int> potentialRoomPositions) {
        int roomsCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        List<Vector2Int> rooms = potentialRoomPositions.OrderBy(p => Guid.NewGuid()).Take(roomsCount).ToList();

        foreach (var room in rooms) {
            var roomFloor = RunRandomWalk(room);
            roomPositions.UnionWith(roomFloor);
        }
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
    #endregion
}
