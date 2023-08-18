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

        List<List<Vector2Int>> corridors = RunCorridorRandomWalk(floorPositions, potentialRoomPositions);
        RunRoomRandomWalk(roomPositions, potentialRoomPositions);

        if (!deadEnds) {
            var deadEndPositions = GetAllDeadEnds(floorPositions);
            CreateRoomAtDeadEnd(deadEndPositions, roomPositions);
        }

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; ++i) {
            corridors[i] = is3x3 ? IncreaseCorridorBrush3by3(corridors[i]) : IncreaseCorridorSizeByOne(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        painter.PaintFloor(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, painter);
    }

    private List<List<Vector2Int>> RunCorridorRandomWalk(HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        List<List<Vector2Int>> corridors = new();

        for (int i = 0; i < corridorCount; ++i) {
            var corridor = ProceduralGenerator.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[^1];

            corridors.Add(corridor);

            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

        return corridors;
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

    private List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor) {
        List<Vector2Int> newCorridor = new();
        Vector2Int previousDirection = Vector2Int.zero;

        for (int i = 1; i < corridor.Count; ++i) {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];

            if (previousDirection != Vector2Int.zero &&
                directionFromCell != previousDirection) {
                // Corner tile
                for (int j = -1; j < 2; ++j) {
                    for (int k = -1; k < 2; ++k) {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(j, k));
                    }
                }
            } else {
                // Add cell in the direction + 90 degrees
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }

            previousDirection = directionFromCell;
        }

        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction) {
        if (direction == Vector2Int.up) {
            return Vector2Int.right;
        } else if (direction == Vector2Int.right) {
            return Vector2Int.down;
        } else if (direction == Vector2Int.down) {
            return Vector2Int.left;
        } else if (direction == Vector2Int.left) {
            return Vector2Int.up;
        }

        return Vector2Int.zero;
    }

    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor) {
        List<Vector2Int> newCorridor = new();

        for (int i = 1; i < corridor.Count; ++i) {
            for (int j = -1; j < 2; ++j) {
                for (int k = -1; k < 2; ++k) {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(j, k));
                }
            }
        }

        return newCorridor;
    }
    #endregion

    #region Member variables
    [SerializeField]
    private int corridorLength = 14;
    [SerializeField]
    private int corridorCount = 5;
    [SerializeField]
    private bool is3x3 = false;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;

    [SerializeField]
    private bool deadEnds = false;
    #endregion
}
