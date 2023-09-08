using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class DecalGenerator : MonoBehaviour {
    #region Methods
    public void Place(HashSet<Vector2Int> positions, TilemapPainter painter) {
        Assert.IsTrue(decals.Length > 0, "Not enough decals given to DecalGenerator");

        int randomDecalsAmount = Random.Range(minDecalsAmount, maxDecalsAmount);
        int currentDecalsAmount = 0;

        while (currentDecalsAmount < randomDecalsAmount) {
            painter.PaintDecal(GetRandomPosition(positions), GetRandomDecal());

            currentDecalsAmount++;
        }
    }

    private Vector2Int GetRandomPosition(HashSet<Vector2Int> positions) {
        Vector2Int randomPosition;

        do {
            randomPosition = positions.ElementAt(Random.Range(0, positions.Count));
        } while (usedPositions.Contains(randomPosition));

        usedPositions.Add(randomPosition);

        return randomPosition;
    }

    private TileBase GetRandomDecal() {
        int randomIndex = Random.Range(0, decals.Length);
        return decals[randomIndex];
    }

    public void ClearPositions() {
        usedPositions.Clear();
    }
    #endregion

    #region Member variables
    [SerializeField]
    private int minDecalsAmount = 30;
    [SerializeField]
    private int maxDecalsAmount = 50;

    [SerializeField]
    private TileBase[] decals;

    private HashSet<Vector2Int> usedPositions = new();
    #endregion
}
