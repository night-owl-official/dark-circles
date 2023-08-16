using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour {
    #region Methods
    public void GenerateDungeon() {
        painter.ClearAll();
        RunProceduralGenerator();
    }

    public void ClearDungeon() {
        RunGeneratorCleanup();
    }

    protected abstract void RunProceduralGenerator();
    protected abstract void RunGeneratorCleanup();
    #endregion

    #region Member variables
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    protected TilemapPainter painter = null;
    #endregion
}
