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

}
