using UnityEngine;

public class CursorManager : MonoBehaviour {
    public enum CursorType {
        Follow
    }

    #region Methods
    // Start is called before the first frame update
    private void Start() {
        Cursor.visible = true;
    }

    // Update is called once per frame
    private void Update() {
        
    }
    #endregion

    #region Member variables
    [SerializeField]
    private CursorSO[] cursors;

    private int currentFrameIndex = 0;
    private float timer = 0f;
    #endregion
}
