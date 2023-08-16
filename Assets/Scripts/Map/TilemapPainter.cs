using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour {
    private enum TileType {
        FLOOR,
        WALL,
        OBSTACLE,
        DECAL
    }

    #region Methods
    public void PaintFloor(IEnumerable<Vector2Int> positions) {
        PaintTiles(positions, floorTilemap, TileType.FLOOR);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileType tileType) {
        ClearTilemap(tilemap);

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
            case TileType.WALL:
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
    #endregion
}
