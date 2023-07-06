using UnityEngine;
using UnityEngine.Assertions;

public class CursorManager : MonoBehaviour {
    public enum CursorType {
        Follow
    }

    #region Methods
    // Start is called before the first frame update
    private void Start() {
        Cursor.visible = true;

        // Default is follow cursor
        SetCurrentCursor(cursors[0]);
    }

    private void SetCurrentCursor(CursorSO cursorSO) {
        Assert.IsNotNull(cursorSO);
        currentCursor = cursorSO;
    }

    // Update is called once per frame
    private void Update() {
        RenderMouseCursor();
    }

    private void RenderMouseCursor() {
        SetFrameIndex();
        SetMouseCursor();
    }

    /// <summary>
    /// Must be called every frame.
    /// </summary>
    private void SetFrameIndex() {
        Assert.IsNotNull(currentCursor);

        timer += Time.deltaTime;
        float frameDuration = 1f / currentCursor.frameRate;

        if (timer >= frameDuration) {
            currentFrameIndex = (currentFrameIndex + 1) % currentCursor.animationFrames.Length;
            timer -= frameDuration;
        }
    }

    private void SetMouseCursor() {
        Assert.IsNotNull(currentCursor);

        Texture2D currentFrame = currentCursor.animationFrames[currentFrameIndex];

        Vector2 hotspot = currentCursor.isHotspotMiddle ?
                          new Vector2(currentFrame.width, currentFrame.height) * 0.5f :
                          currentCursor.hotspot;

        Cursor.SetCursor(currentFrame, hotspot, CursorMode.Auto);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private CursorSO[] cursors;

    private int currentFrameIndex = 0;
    private float timer = 0f;

    private CursorSO currentCursor = null;
    #endregion
}
