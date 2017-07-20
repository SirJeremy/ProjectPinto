using UnityEngine;
using UnityEditor;

public class BoardTileSetEditorWindow : EditorWindow {
    public Object objTS = null;
    public BoardTileSet boardTileSet = null;
    public int width = 2;
    public int height = 3;
    public int numberUIB = 0;
    public TileSetData tiles = null;

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
            boardTileSet = (BoardTileSet)objTS;
            numberUIB = boardTileSet.hasUIButtons ? boardTileSet.uiButtonColors.Length : 0;

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
            int tmp = EditorGUILayout.IntField("X:", boardTileSet.playerStartingPosition.X);
            boardTileSet.playerStartingPosition = new IndexVector(tmp, EditorGUILayout.IntField("Y:", boardTileSet.playerStartingPosition.Y));
            EditorGUILayout.EndHorizontal();

            //UIButtons row
            EditorGUILayout.BeginHorizontal();
            numberUIB = Mathf.Clamp(EditorGUILayout.DelayedIntField("Number of UI Buttons", numberUIB), 0, 3);
            if(numberUIB == 0) {
                boardTileSet.hasUIButtons = false;
            }
            else {
                boardTileSet.hasUIButtons = true;
                if(boardTileSet.uiButtonColors.Length != numberUIB)
                    boardTileSet.uiButtonColors = new EColor[numberUIB];
                for(int i = 0; i < numberUIB; i++) {
                    boardTileSet.uiButtonColors[i] = (EColor)EditorGUILayout.EnumPopup(boardTileSet.uiButtonColors[i]);
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Note: (0,0) is the bottom left. X+ is right and Y+ is up.");

            tiles = boardTileSet.tiles;
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
