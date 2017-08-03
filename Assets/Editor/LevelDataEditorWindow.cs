using UnityEngine;
using UnityEditor;

public class LevelDataEditorWindow : EditorWindow {
    public Object objLD = null;
    public LevelData levelData = null;
    public int width = 2;
    public int height = 3;
    public int numberUIB = 0;
    public TileSetData tiles = null;

    [MenuItem("Window/Custom/Level Editor")]
    public static void ShowWindow() {
        LevelDataEditorWindow window = GetWindow<LevelDataEditorWindow>("Level Editor");
        if(Selection.activeObject != null && Selection.activeObject.GetType() == typeof(LevelData))
            window.objLD = Selection.activeObject;
        window.position = new Rect(50, 50, 600, 300);
        window.minSize = new Vector2(600, 100);
        window.Show();
    }

    private void OnGUI() {
        objLD = EditorGUILayout.ObjectField("Level Data", objLD, typeof(LevelData), false);

        if(objLD != null) {
            levelData = (LevelData)objLD;
            numberUIB = levelData.hasUIButtons ? levelData.uiButtonColors.Length : 0;

            //Name row
            EditorGUILayout.BeginHorizontal();
            levelData.levelName = EditorGUILayout.TextField("Name ", levelData.levelName);
            EditorGUILayout.EndHorizontal();

            //Size row
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Load Size"))
                LoadTileSetSize();
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
            if(GUILayout.Button("Update Size"))
                ChangeTileSetSize();
            EditorGUILayout.EndHorizontal();

            //Player starting position row
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Player Starting Position ");
            int tmp = EditorGUILayout.IntField("X:", levelData.playerStartingPosition.X);
            levelData.playerStartingPosition = new IndexVector(tmp, EditorGUILayout.IntField("Y:", levelData.playerStartingPosition.Y));
            EditorGUILayout.EndHorizontal();

            //UIButtons row
            EditorGUILayout.BeginHorizontal();
            numberUIB = Mathf.Clamp(EditorGUILayout.DelayedIntField("Number of UI Buttons", numberUIB), 0, 3);
            if(numberUIB == 0) {
                levelData.hasUIButtons = false;
            }
            else {
                levelData.hasUIButtons = true;
                if(levelData.uiButtonColors.Length != numberUIB)
                    levelData.uiButtonColors = new EColor[numberUIB];
                for(int i = 0; i < numberUIB; i++) {
                    levelData.uiButtonColors[i] = (EColor)EditorGUILayout.EnumPopup(levelData.uiButtonColors[i]);
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Note: (0,0) is the bottom left. X+ is right and Y+ is up.");

            tiles = levelData.tiles;
            if(tiles != null) {
                for(int y = tiles.height - 1; y >= 0; y--) {
                    EditorGUILayout.BeginHorizontal();
                    for(int x = 0; x < tiles.width; x++) {
                        tiles.rows[x].column[y].type = (ETile)EditorGUILayout.EnumPopup(tiles.rows[x].column[y].type);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    for(int x = 0; x < tiles.width; x++) {
                        tiles.rows[x].column[y].color = (EColor)EditorGUILayout.EnumPopup(tiles.rows[x].column[y].color);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            }
        }
    }

    private void OnLostFocus() {
        EditorUtility.SetDirty(levelData);
    }

    private void LoadTileSetSize() {
        if(tiles != null)
        width = tiles.width;
        height = tiles.height;
    }
    private void ChangeTileSetSize() {
        if(tiles != null) {
            tiles.UpdateTableSize(width, height);
        }
    }
}
