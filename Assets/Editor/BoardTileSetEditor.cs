using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardTileSet))]
public class BoardTileSetEditor : Editor {
    public override void OnInspectorGUI() {
        if(GUILayout.Button("Open Editor Window"))
            OpenEditWindow();
        base.OnInspectorGUI();
    }

    private void OpenEditWindow() {
        BoardTileSetEditorWindow.ShowWindow();
    }
}
