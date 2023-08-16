using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class DungeonGeneratorEditor : Editor {
    private AbstractDungeonGenerator dungeonGenerator;

    private void Awake() {
        dungeonGenerator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Dungeon")) {
            dungeonGenerator.GenerateDungeon();
        }

        if (GUILayout.Button("Clear Dungeon")) {
            dungeonGenerator.ClearDungeon();
        }
    }
}
