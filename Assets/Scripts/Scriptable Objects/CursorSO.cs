using UnityEngine;

[CreateAssetMenu(fileName = "New Cursor", menuName = "DarkCircles/Cursor")]
public class CursorSO : ScriptableObject {
    public CursorManager.CursorType type;

    public Texture2D[] animationFrames;
    public float frameRate;

    public bool isHotspotMiddle;
    public Vector2 hotspot;
}
