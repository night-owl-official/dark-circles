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
}

public static class Direction2D {
    public static List<Vector2Int> cardinalDirections = new() {
        new Vector2Int(0, 1), // UP
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) // LEFT
    };

    public static Vector2Int GetRandomCardinalDirection() {
        return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
    }
}
