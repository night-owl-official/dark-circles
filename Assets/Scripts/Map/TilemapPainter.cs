using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapPainter : MonoBehaviour {
    private enum TileType {
        NONE,
        FLOOR,
        WALL_TOP,
        WALL_RIGHT,
        WALL_DOWN,
        WALL_LEFT,
        WALL_INNER_DOWN_RIGHT,
        WALL_INNER_DOWN_LEFT,
        WALL_DIAGONAL_TOP_RIGHT,
        WALL_DIAGONAL_DOWN_RIGHT,
        WALL_DIAGONAL_TOP_LEFT,
        WALL_DIAGONAL_DOWN_LEFT,
        WALL_FULL,
        OBSTACLE,
        DECAL
    }

    #region Methods
    public void ClearAll() {
        Assert.IsNotNull(floorTilemap, "A tilemap for the floor is missing in TilemapPainter");

        floorTilemap.ClearAllTiles();
        wallsTilemap.ClearAllTiles();
        decalsTilemap.ClearAllTiles();
        obstaclesTilemap.ClearAllTiles();
    }

    public Vector3 GetTileCenter(Vector2Int position) {
        Vector3Int tileCellPosition = floorTilemap.WorldToCell((Vector3Int) position);
        return floorTilemap.GetCellCenterWorld(tileCellPosition);
    }

    public void PaintBasicWall(Vector2Int wallPosition, string wallType) {
        TileType tileType = TileType.NONE;
        int wallTypeBinary = Convert.ToInt32(wallType, 2);

        if (WallTypesHelper.wallTop.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_TOP;
        } else if (WallTypesHelper.wallSideRight.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_RIGHT;
        } else if (WallTypesHelper.wallBottm.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_DOWN;
        } else if (WallTypesHelper.wallSideLeft.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_LEFT;
        } else if (WallTypesHelper.wallFull.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_FULL;
        }

        if (tileType != TileType.NONE)
            PaintTile(wallPosition, wallsTilemap, tileType);
    }

    public void PaintCornerWall(Vector2Int wallPosition, string wallType) {
        TileType tileType = TileType.NONE;
        int wallTypeBinary = Convert.ToInt32(wallType, 2);

        if (WallTypesHelper.wallInnerCornerDownRight.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_INNER_DOWN_RIGHT;
        } else if (WallTypesHelper.wallInnerCornerDownLeft.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_INNER_DOWN_LEFT;
        } else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_DIAGONAL_TOP_RIGHT;
        } else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_DIAGONAL_DOWN_RIGHT;
        } else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_DIAGONAL_DOWN_LEFT;
        } else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_DIAGONAL_TOP_LEFT;
        } else if (WallTypesHelper.wallFullEightDirections.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_FULL;
        } else if (WallTypesHelper.wallBottmEightDirections.Contains(wallTypeBinary)) {
            tileType = TileType.WALL_DOWN;
        }

        if (tileType != TileType.NONE)
            PaintTile(wallPosition, wallsTilemap, tileType);
    }

    public void PaintFloor(IEnumerable<Vector2Int> positions) {
        PaintTiles(positions, floorTilemap, TileType.FLOOR);
    }

    public void PaintDecal(Vector2Int position, TileBase decal) {
        Assert.IsNotNull(decalsTilemap, "Decals tilemap missing in TilemapPainter");

        Vector3Int tileCellPosition = decalsTilemap.WorldToCell((Vector3Int) position);
        decalsTilemap.SetTile(tileCellPosition, decal);
    }

    public void PaintObstacle(Vector2Int position, TileBase obstacle) {
        Assert.IsNotNull(obstaclesTilemap, "Obstacles tilemap missing in TilemapPainter");

        Vector3Int tileCellPosition = obstaclesTilemap.WorldToCell((Vector3Int) position);
        obstaclesTilemap.SetTile(tileCellPosition, obstacle);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileType tileType) {
        foreach (Vector2Int position in positions) {
            PaintTile(position, tilemap, tileType);
        }
    }

    private void ClearTilemap(Tilemap tilemap) {
        Assert.IsNotNull(tilemap, "Tilemap is missing when trying to clear tiles");

        tilemap.ClearAllTiles();
    }

    private void PaintTile(Vector2Int position, Tilemap tilemap, TileType tileType) {
        Vector3Int tileCellPosition = tilemap.WorldToCell((Vector3Int) position);
        tilemap.SetTile(tileCellPosition, GetTileFromTileType(tileType));
    }

    private TileBase GetTileFromTileType(TileType tileType) {
        TileBase tile = null;

        switch (tileType) {
            case TileType.FLOOR:
                tile = floorTiles[Random.Range(0, floorTiles.Length)];
                break;
            case TileType.WALL_TOP:
                tile = wallTop[Random.Range(0, wallTop.Length)];
                break;
            case TileType.WALL_RIGHT:
                tile = wallRight[Random.Range(0, wallRight.Length)];
                break;
            case TileType.WALL_DOWN:
                tile = wallDown[Random.Range(0, wallDown.Length)];
                break;
            case TileType.WALL_LEFT:
                tile = wallLeft[Random.Range(0, wallLeft.Length)];
                break;
            case TileType.WALL_FULL:
                tile = wallFull[Random.Range(0, wallFull.Length)];
                break;
            case TileType.WALL_INNER_DOWN_RIGHT:
                tile = wallInnerCornerDownRight;
                break;
            case TileType.WALL_INNER_DOWN_LEFT:
                tile = wallInnerCornerDownLeft;
                break;
            case TileType.WALL_DIAGONAL_TOP_RIGHT:
                tile = wallDiagonalCornerTopRight;
                break;
            case TileType.WALL_DIAGONAL_DOWN_RIGHT:
                tile = wallDiagonalCornerDownRight;
                break;
            case TileType.WALL_DIAGONAL_DOWN_LEFT:
                tile = wallDiagonalCornerDownLeft;
                break;
            case TileType.WALL_DIAGONAL_TOP_LEFT:
                tile = wallDiagonalCornerTopLeft;
                break;
            case TileType.OBSTACLE:
                break;
            case TileType.DECAL:
                break;
        }

        return tile;
    }
    #endregion

    #region Member variables
    [Header("Floor")]

    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private TileBase[] floorTiles;

    [Header("Walls")]

    [SerializeField]
    private Tilemap wallsTilemap;
    [SerializeField]
    private TileBase[] wallTop;
    [SerializeField]
    private TileBase[] wallRight;
    [SerializeField]
    private TileBase[] wallDown;
    [SerializeField]
    private TileBase[] wallLeft;
    [SerializeField]
    private TileBase wallInnerCornerDownRight;
    [SerializeField]
    private TileBase wallInnerCornerDownLeft;
    [SerializeField]
    private TileBase wallDiagonalCornerTopRight;
    [SerializeField]
    private TileBase wallDiagonalCornerDownRight;
    [SerializeField]
    private TileBase wallDiagonalCornerTopLeft;
    [SerializeField]
    private TileBase wallDiagonalCornerDownLeft;
    [SerializeField]
    private TileBase[] wallFull;

    [Header("Decals")]

    [SerializeField]
    private Tilemap decalsTilemap;

    [Header("Obstacles")]

    [SerializeField]
    private Tilemap obstaclesTilemap;
    #endregion
}
