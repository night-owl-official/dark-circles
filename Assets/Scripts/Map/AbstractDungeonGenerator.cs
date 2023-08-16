using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour {
    #region Methods
    public void GenerateDungeon() {
        RunProceduralGenerator();
    }

    protected abstract void RunProceduralGenerator();
    #endregion

    #region Member variables
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    protected TilemapPainter painter = null;
    #endregion
}
