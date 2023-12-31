using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerator {
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPosition, int walkLength) {
        HashSet<Vector2Int> path = new() {
            startPosition
        };
        Vector2Int currentPosition = startPosition;

        for (int i = 0; i < walkLength; ++i) {
            Vector2Int newPosition = currentPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            currentPosition = newPosition;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength) {
        List<Vector2Int> corridor = new();

        Vector2Int currentPosition = startPosition;
        corridor.Add(currentPosition);

        Vector2Int direction = Direction2D.GetRandomCardinalDirection();

        for (int i = 0; i < corridorLength; ++i) {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}

public static class Direction2D {
    public static List<Vector2Int> cardinalDirections = new() {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) // LEFT
    };

    public static List<Vector2Int> diagonalDirections = new() {
        new Vector2Int(1, 1), // UP-RIGHT
        new Vector2Int(1, -1), // RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) // LEFT-UP
    };

    public static List<Vector2Int> fullDirections = new() {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 1), // UP-RIGHT
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(1, -1), // RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), // LEFT
        new Vector2Int(-1, 1) // LEFT-UP
    };

    public static Vector2Int GetRandomCardinalDirection() {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }

    public static Vector2Int GetRandomFullDirection() {
        return fullDirections[Random.Range(0, fullDirections.Count)];
    }
}
