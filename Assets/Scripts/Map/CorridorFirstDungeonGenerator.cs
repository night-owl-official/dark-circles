using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : RandomWalkDungeonGenerator {
    public enum CorridorSize {
        oneByOne,
        twoByTwo,
        threeByThree
    }

    #region Methods
    private void OnDrawGizmos() {
        if (!showGizmos) return;

        for (int i = 0; i < roomsDictionary.Keys.Count; ++i) {
            Gizmos.color = randomColors[i];

            var key = roomsDictionary.Keys.ElementAt(i);
            foreach (var roomTile in roomsDictionary[key]) {
                Gizmos.DrawCube(painter.GetTileCenter(roomTile), Vector3.one);
            }
        }
        
    }

    protected override void RunGeneratorCleanup() {
        base.RunGeneratorCleanup();

        ClearDungeonData();
    }

    protected override void RunProceduralGenerator() {
        RunCorridorFirstDungeonGeneration();
    }

    private void RunCorridorFirstDungeonGeneration() {
        ClearDungeonData();

        HashSet<Vector2Int> potentialRoomPositions = new();
        List<HashSet<Vector2Int>> corridors = CreateCorridors(potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        TryGeneratingRoomsAtDeadEnds(roomPositions);
        TryIncreasingCorridorSize(corridors);

        painter.PaintFloor(floorPositions);
        WallGenerator.GenerateWalls(floorPositions, painter);
    }


    private void ClearDungeonData() {
        floorPositions.Clear();
        corridorPositions.Clear();
        roomsDictionary.Clear();
        randomColors.Clear();
    }

    private List<HashSet<Vector2Int>> CreateCorridors(HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        List<HashSet<Vector2Int>> corridors = new();

        for (int i = 0; i < corridorCount; ++i) {
            var corridor = ProceduralGenerator.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[^1];

            corridors.Add(corridor.ToHashSet());

            potentialRoomPositions.Add(currentPosition);
            corridorPositions.UnionWith(corridor.ToHashSet());
            floorPositions.UnionWith(corridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions) {
        int roomsCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
        List<Vector2Int> rooms = potentialRoomPositions.OrderBy(p => Guid.NewGuid()).Take(roomsCount).ToList();

        HashSet<Vector2Int> roomPositions = new();

        foreach (var room in rooms) {
            var roomFloor = RunRandomWalk(room);
            roomPositions.UnionWith(roomFloor);

            roomsDictionary.Add(room, roomFloor);
            randomColors.Add(UnityEngine.Random.ColorHSV());
        }

        return roomPositions;
    }

    private void TryGeneratingRoomsAtDeadEnds(HashSet<Vector2Int> roomPositions) {
        if (!deadEnds) {
            var deadEndPositions = GetAllDeadEnds();
            CreateRoomAtDeadEnd(deadEndPositions, roomPositions);
            floorPositions.UnionWith(roomPositions);
        }
    }

    private List<Vector2Int> GetAllDeadEnds() {
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

                roomsDictionary.Add(deadEndPosition, room);
                randomColors.Add(UnityEngine.Random.ColorHSV());
            }
        }
    }

    private void TryIncreasingCorridorSize(List<HashSet<Vector2Int>> corridors) {
        if (corridorSize == CorridorSize.oneByOne)
            return;

        bool is3x3 = corridorSize != CorridorSize.twoByTwo;

        for (int i = 0; i < corridors.Count; ++i) {
            corridors[i] = is3x3 ? IncreaseCorridorBrush3by3(corridors[i]) : IncreaseCorridorSizeByOne(corridors[i]);
            corridorPositions.UnionWith(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }
    }

    private HashSet<Vector2Int> IncreaseCorridorSizeByOne(HashSet<Vector2Int> corridor) {
        HashSet<Vector2Int> newCorridor = new();
        Vector2Int previousDirection = Vector2Int.zero;

        for (int i = 1; i < corridor.Count; ++i) {
            Vector2Int directionFromCell = corridor.ElementAt(i) - corridor.ElementAt(i - 1);

            if (previousDirection != Vector2Int.zero &&
                directionFromCell != previousDirection) {
                // Corner tile
                for (int j = -1; j < 2; ++j) {
                    for (int k = -1; k < 2; ++k) {
                        newCorridor.Add(corridor.ElementAt(i - 1) + new Vector2Int(j, k));
                    }
                }
            } else {
                // Add cell in the direction + 90 degrees
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor.ElementAt(i - 1));
                newCorridor.Add(corridor.ElementAt(i - 1) + newCorridorTileOffset);
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

    private HashSet<Vector2Int> IncreaseCorridorBrush3by3(HashSet<Vector2Int> corridor) {
        HashSet<Vector2Int> newCorridor = new();

        for (int i = 1; i < corridor.Count; ++i) {
            for (int j = -1; j < 2; ++j) {
                for (int k = -1; k < 2; ++k) {
                    newCorridor.Add(corridor.ElementAt(i - 1) + new Vector2Int(j, k));
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
    private CorridorSize corridorSize = CorridorSize.oneByOne;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float roomPercent = 0.8f;

    [SerializeField]
    private bool deadEnds = false;

    [Space(32)]
    [Header("Debug")]
    [SerializeField]
    private bool showGizmos = false;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new();
    private HashSet<Vector2Int> floorPositions = new();
    private HashSet<Vector2Int> corridorPositions = new();

    // Debug
    private List<Color> randomColors = new();
    #endregion
}
