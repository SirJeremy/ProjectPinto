using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor {
    public override void OnInspectorGUI() {
        if(GUILayout.Button("Open Editor Window"))
            OpenEditWindow();
        base.OnInspectorGUI();
    }

    private void OpenEditWindow() {
        LevelDataEditorWindow.ShowWindow();
    }
}
