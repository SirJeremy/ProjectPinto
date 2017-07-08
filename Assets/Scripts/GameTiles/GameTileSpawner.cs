using UnityEngine;
using System.Collections.Generic;

public static class GameTileSpawner {
    private static Dictionary<ETile, GameObject> prefabs;
    private static Transform boardHolder = null;

    private static Transform BoardHolder {
        get {
            if(boardHolder == null) {
                boardHolder = Object.Instantiate(new GameObject("GameBoard"), Vector3.zero, Quaternion.identity).GetComponent<Transform>();
            }
            return boardHolder;
        }
    }

    static GameTileSpawner() {
        InitializePrefabs();
    }

    private static void InitializePrefabs() {
        string basePath = "Prefabs/GameTiles/";
        string empty = "Empty";
        string wall = "Wall";
        string goal = "Goal";
        if(prefabs == null)
            prefabs = new Dictionary<ETile, GameObject>();

        prefabs.Add(ETile.EMPTY, Resources.Load<GameObject>(basePath + empty));
        prefabs.Add(ETile.WALL, Resources.Load<GameObject>(basePath + wall));
        prefabs.Add(ETile.GOAL, Resources.Load<GameObject>(basePath + goal));
    }
    public static GameObject SpawnGameTile(ETile tile, IndexVector location, out bool isTraverseable, out bool canChangeTraversability) {
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
            isTraverseable = gt.IsTraverseable;
            canChangeTraversability = gt.CanChangeTraversability;
        }
        return go;
    }

}
