using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator {
    public static void GenerateWalls(HashSet<Vector2Int> floorPositions, TilemapPainter painter) {
        var basicWallPositions = GetWallPositions(floorPositions, Direction2D.cardinalDirections);
        var cornerWallPositions = GetWallPositions(floorPositions, Direction2D.diagonalDirections);

        CreateBasicWalls(basicWallPositions, floorPositions, painter);
        CreateCornerWalls(cornerWallPositions, floorPositions, painter);
    }

    private static void CreateBasicWalls(HashSet<Vector2Int> basicWallPositions,
        HashSet<Vector2Int> floorPositions,
        TilemapPainter painter) {
        foreach (var wallPosition in basicWallPositions) {
            string neighborsBinaryType = "";

            foreach (var direction in Direction2D.cardinalDirections) {
                var neighborPosition = wallPosition + direction;
                if (floorPositions.Contains(neighborPosition)) {
                    neighborsBinaryType += "1";
                } else {
                    neighborsBinaryType += "0";
                }
            }

            painter.PaintBasicWall(wallPosition, neighborsBinaryType);
        }
    }

    private static void CreateCornerWalls(HashSet<Vector2Int> cornerWallPositions,
        HashSet<Vector2Int> floorPositions,
        TilemapPainter painter) {
        foreach (var wallPosition in cornerWallPositions) {
            string neighborsBinaryType = "";

            foreach (var direction in Direction2D.fullDirections) {
                var neighborPosition = wallPosition + direction;
                if (floorPositions.Contains(neighborPosition)) {
                    neighborsBinaryType += "1";
                } else {
                    neighborsBinaryType += "0";
                }
            }

            painter.PaintCornerWall(wallPosition, neighborsBinaryType);
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
