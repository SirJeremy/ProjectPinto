using UnityEngine;
using System.Collections.Generic;

public static class GameTileSpawner {
    private static Dictionary<ETile, GameObject> prefabs;
    private static Transform boardHolder = null;

    private static Transform BoardHolder {
        get {
            if(boardHolder == null) {
                boardHolder = new GameObject("GameBoard").transform;
            }
            return boardHolder;
        }
    }

    static GameTileSpawner() {
        InitializePrefabs();
    }

    private static void InitializePrefabs() {
        GameTileSet gts;
        string path = "GameTiles";

        gts = Resources.Load<GameTileSet>(path);

        if(prefabs == null)
            prefabs = new Dictionary<ETile, GameObject>();

        prefabs.Add(ETile.EMPTY, gts.GetPrefab(ETile.EMPTY));
        prefabs.Add(ETile.WALL, gts.GetPrefab(ETile.WALL));
        prefabs.Add(ETile.GOAL, gts.GetPrefab(ETile.GOAL));
        prefabs.Add(ETile.BUTTON, gts.GetPrefab(ETile.BUTTON));
        prefabs.Add(ETile.GATE, gts.GetPrefab(ETile.GATE));
    }
    public static GameObject SpawnGameTile(ETile tile, EColor color, IndexVector location, out bool isTraverseable, out bool canChangeTraversability) {
        GameObject go = Object.Instantiate(prefabs[tile], location.ToVector3, Quaternion.identity, BoardHolder);
        GameTile gt = go.GetComponent<GameTile>();
        go.name = go.name + " " + location.ToString();
        if(gt == null) {
            Debug.LogError("No GameTile attached to GameObject " + go.name);
            isTraverseable = true;
            canChangeTraversability = false;
        }   
        else {
            gt.Location = location;
            gt.SetColor(color);
            isTraverseable = gt.IsTraverseable; //stored in prefab
            canChangeTraversability = gt.CanChangeTraversability; //stored in prefab
        }
        return go;
    }
    public static GameObject SpawnGameBoardBorder(int boardWidth, int  boardHeight) {
        GameObject border;
        Mesh mesh;
        float extra = 25; //number of units the border extends extra
        float widthNorm = 1 / (boardWidth + extra);
        float heightNorm = 1 / (boardHeight + extra);
        //Create GameObject
        border = new GameObject("Border");
        //Add components
        border.AddComponent<MeshFilter>();
        border.AddComponent<MeshRenderer>();
        //Set parent
        border.transform.parent = BoardHolder;
        //Set material
        border.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Border");
        //Offset position to make verts nicer to calc (no .5 ofset for all verts)
        border.transform.position = new Vector3(-.5f, .5f, -.5f);
        //Get mesh ref
        mesh = border.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        //Create verts
        mesh.vertices = new Vector3[] { /* v1:0, v2:right, v3:up, v4:right-up  */
            /* Outer verts (0) */ new Vector3(-extra, 0, -extra), new Vector3(boardWidth + extra, 0, -extra), new Vector3(-extra, 0, boardHeight + extra), new Vector3(boardWidth + extra, 0, boardHeight + extra),
            /* Inner verts (4) */ Vector3.zero, new Vector3(boardWidth, 0, 0), new Vector3(0, 0, boardHeight), new Vector3(boardWidth, 0, boardHeight),
            /* Lower verts (8) */ Vector3.down, new Vector3(boardWidth, -1, 0), new Vector3(0, -1, boardHeight), new Vector3(boardWidth, -1, boardHeight)
        };
        //Set uvs
        mesh.uv = new Vector2[12];
        for(int i = 0; i < 12; i++) {
            mesh.uv[i] = new Vector2((mesh.vertices[i].x + extra + boardWidth) * widthNorm, (mesh.vertices[i].z + extra + boardHeight)* heightNorm);
        }
        //Set triangles vert tri-pair via i of verticies
        mesh.triangles = new int[] { /*Outer left*/ 4, 0, 2, 4, 2, 6, /*Outer up*/ 6, 2, 3, 6, 3, 7, /*Outer right*/ 7, 3, 1, 7, 1, 5, /*Outer down*/ 5, 1, 0, 5, 0, 4,
            /*Inner left*/ 8, 4, 6, 8, 6, 10, /*Inner up*/ 10, 6, 7, 10, 7, 11, /*Inner right*/ 11, 7, 5, 11, 5, 9, /*Inner down*/ 9, 5, 4, 9, 4, 8,
            /*center*/ 8, 10, 11, 8, 11, 9
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return border;
    }

}
