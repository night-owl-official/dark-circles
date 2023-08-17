using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator {
    public static void GenerateWalls(HashSet<Vector2Int> floorPositions, TilemapPainter painter) {
        var wallPositions = GetWallPositions(floorPositions, Direction2D.cardinalDirections);

        foreach (var position in wallPositions) {
            painter.PaintSingleWall(position);
        }
    }

    private static HashSet<Vector2Int> GetWallPositions(HashSet<Vector2Int> floorPositions,
        List<Vector2Int> directions) {
        HashSet<Vector2Int> wallPositions = new();

        foreach (var position in floorPositions) {
            foreach (var direction in directions) {
                var neighborTilePosition = position + direction;

                if (!floorPositions.Contains(neighborTilePosition)) {
                    wallPositions.Add(neighborTilePosition);
                }
            }
        }

        return wallPositions;
    }
}
