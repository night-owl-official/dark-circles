using UnityEngine;

public class CursorManager : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        Cursor.visible = true;

        Cursor.SetCursor(cursorTexture,
            new Vector2(cursorTexture.width, cursorTexture.height) * 0.5f,
            CursorMode.Auto);
    }

    // Update is called once per frame
    private void Update() {
        
    }
    #endregion

    #region Member variables
    [SerializeField]
    private Texture2D cursorTexture = null;
    #endregion
}
