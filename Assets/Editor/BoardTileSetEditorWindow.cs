using UnityEngine;
using UnityEditor;

public class BoardTileSetEditorWindow : EditorWindow {
    public Object objTS = null;
    public BoardTileSet tileSet = null;
    public int width = 2;
    public int height = 3;
    public TileData tiles = null;

    [MenuItem("Window/Custom/Tile Set Editor")]
    public static void ShowWindow() {
        BoardTileSetEditorWindow window = GetWindow<BoardTileSetEditorWindow>("Tile Set Editor");
        if(Selection.activeObject != null && Selection.activeObject.GetType() == typeof(BoardTileSet))
            window.objTS = Selection.activeObject;
        window.position = new Rect(50, 50, 600, 300);
        window.minSize = new Vector2(600, 100);
        window.Show();
    }

    private void OnGUI() {
        objTS = EditorGUILayout.ObjectField("Tile Set", objTS, typeof(BoardTileSet), false);

        if(objTS != null) {
            tileSet = (BoardTileSet)objTS;

            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Load Size"))
                LoadTileSetSize();
            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);
            if(GUILayout.Button("Update Size"))
                ChangeTileSetSize();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Player Starting Position ");
            int tmp = EditorGUILayout.IntField("X:", tileSet.playerStartingPosition.X);
            tileSet.playerStartingPosition = new IndexVector(tmp, EditorGUILayout.IntField("Y:", tileSet.playerStartingPosition.Y));
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Note: (0,0) is the bottom left. X+ is right and Y+ is up.");

            tiles = tileSet.tiles;
            if(tiles != null) {
                for(int y = tiles.height - 1; y >= 0; y--) {
                    EditorGUILayout.BeginHorizontal();
                    for(int x = 0; x < tiles.width; x++) {
                        tiles.rows[x].column[y] = (ETile)EditorGUILayout.EnumPopup(tiles.rows[x].column[y]);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
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
