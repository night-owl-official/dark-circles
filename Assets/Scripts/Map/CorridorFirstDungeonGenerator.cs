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

        if (!deadEnds) {
            var deadEndPositions = GetAllDeadEnds(floorPositions);
            CreateRoomAtDeadEnd(deadEndPositions, roomPositions);
        }

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

    private List<Vector2Int> GetAllDeadEnds(HashSet<Vector2Int> floorPositions) {
        List<Vector2Int> deadEndPositions = new();

        foreach (var floorPosition in floorPositions) {
            int neighborsCount = 0;

            foreach (var direction in Direction2D.cardinalDirections) {
                if (floorPositions.Contains(floorPosition + direction)) {
                    neighborsCount++;
                }
            }

            if (neighborsCount == 1) {
                deadEndPositions.Add(floorPosition);
            }
        }

        return deadEndPositions;
    }

    private void CreateRoomAtDeadEnd(List<Vector2Int> deadEndPositions,
        HashSet<Vector2Int> roomFloors) {
        foreach (var deadEndPosition in deadEndPositions) {
            if (!roomFloors.Contains(deadEndPosition)) {
                var room = RunRandomWalk(deadEndPosition);
                roomFloors.UnionWith(room);
            }
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

    [SerializeField]
    private bool deadEnds = false;
    #endregion
}
