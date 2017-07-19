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
        Vector3[] verts;
        Vector2[] uvs;
        int[] tris;
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
        //Create verts
        verts = new Vector3[] { 
            /* Outer verts (0) */ new Vector3(-extra, 0, -extra), new Vector3(boardWidth + extra, 0, -extra), new Vector3(-extra, 0, boardHeight + extra), new Vector3(boardWidth + extra, 0, boardHeight + extra),
            /* Ivert 0 (4) */ Vector3.zero, Vector3.zero, Vector3.zero,
            /* Ivert x (7) */ new Vector3(boardWidth, 0, 0), new Vector3(boardWidth, 0, 0), new Vector3(boardWidth, 0, 0), 
            /* Ivert y (10) */ new Vector3(0, 0, boardHeight), new Vector3(0, 0, boardHeight), new Vector3(0, 0, boardHeight), 
            /* Ivert xy (13) */ new Vector3(boardWidth, 0, boardHeight), new Vector3(boardWidth, 0, boardHeight), new Vector3(boardWidth, 0, boardHeight), 
            /* Lvert 0 (16) */ Vector3.down, Vector3.down, Vector3.down, 
            /* Lvert x (19) */ new Vector3(boardWidth, -1, 0), new Vector3(boardWidth, -1, 0), new Vector3(boardWidth, -1, 0), 
            /* Lvert y (22) */ new Vector3(0, -1, boardHeight), new Vector3(0, -1, boardHeight), new Vector3(0, -1, boardHeight), 
            /* Lvert xy (25) */ new Vector3(boardWidth, -1, boardHeight), new Vector3(boardWidth, -1, boardHeight), new Vector3(boardWidth, -1, boardHeight), 
        };
        //Set uvs
        uvs = new Vector2[28];
        for(int i = 0; i < 28; i++) {
            uvs[i] = new Vector2((verts[i].x + extra + boardWidth) * widthNorm, (verts[i].z + extra + boardHeight)* heightNorm);
        }
        //Set triangles vert tri-pair via i of verticies
        tris = new int[] { /*Outer left*/ 4,0,2,4,2,10, /*Outer up*/ 10,2,3,10,3,13, /*Outer right*/ 13,3,1,13,1,7, /*Outer down*/ 7,1,0,7,0,4,
            /*Inner left*/ 16,5,12,16,12,22, /*Inner up*/ 23,11,15,23,15,25, /*Inner right*/ 26,14,9,26,9,20, /*Inner down*/ 19,8,6,19,6,17,
            /*center*/ 18,24,27,18,27,21
        };
        mesh.Clear();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = tris;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        return border;
    }

}
