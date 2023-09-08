using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class DecorationGenerator : MonoBehaviour {
    #region Methods
    public virtual void Place(HashSet<Vector2Int> positions, TilemapPainter painter) {
        Assert.IsTrue(decorations.Length > 0, "Not enough decorations assigned to DecorationGenerator");

        int randomDecorationsAmount = Random.Range(minAmount, maxAmount);
        int currentDecorationsAmount = 0;

        while (currentDecorationsAmount < randomDecorationsAmount) {
            painter.PaintDecal(GetRandomPosition(positions), GetRandomDecoration());

            currentDecorationsAmount++;
        }
    }

    protected virtual Vector2Int GetRandomPosition(HashSet<Vector2Int> positions) {
        Vector2Int randomPosition;

        do {
            randomPosition = positions.ElementAt(Random.Range(0, positions.Count));
        } while (usedPositions.Contains(randomPosition));

        usedPositions.Add(randomPosition);

        return randomPosition;
    }

    protected virtual TileBase GetRandomDecoration() {
        int randomIndex = Random.Range(0, decorations.Length);
        return decorations[randomIndex];
    }

    public HashSet<Vector2Int> GetUsedPositions() {
        return usedPositions;
    }

    public void ClearPositions() {
        usedPositions.Clear();
    }
    #endregion

    #region Member variables
    [SerializeField]
    protected int minAmount;
    [SerializeField]
    protected int maxAmount;

    [SerializeField]
    protected TileBase[] decorations;

    protected HashSet<Vector2Int> usedPositions = new();
    #endregion
}
