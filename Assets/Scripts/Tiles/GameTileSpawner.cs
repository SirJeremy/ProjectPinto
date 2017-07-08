using UnityEngine;
using System.Collections.Generic;

public static class GameTileSpawner {
    private static Dictionary<ETile, GameObject> prefabs;

    static GameTileSpawner() {
        InitializePrefabs();
    }

    private static void InitializePrefabs() {
        string basePath = "GameTiles/";
        string empty = "Empty";
        string wall = "Wall";
        string goal = "Goal";
        if(prefabs == null)
            prefabs = new Dictionary<ETile, GameObject>();

        prefabs.Add(ETile.EMPTY, Resources.Load<GameObject>(basePath + empty));
        prefabs.Add(ETile.EMPTY, Resources.Load<GameObject>(basePath + wall));
        prefabs.Add(ETile.EMPTY, Resources.Load<GameObject>(basePath + goal));
    }
    public static GameObject SpawnGameTile(ETile tile, IndexVector location) {
        GameObject go = Object.Instantiate(prefabs[tile], location.ToVector3, Quaternion.identity);
        //tell go of its gameTile location
        return go;
    }

}
