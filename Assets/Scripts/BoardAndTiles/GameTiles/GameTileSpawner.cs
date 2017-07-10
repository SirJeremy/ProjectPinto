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

        string buttonsRoot = "Buttons/";
        string buttonBlue = "ButtonBlue";
        string buttonRed = "ButtonRed";
        string buttonYellow = "ButtonYellow";

        string gatesRoot = "Gates/";
        string gateBlue = "GateBlue"; ;
        string gateRed = "GateRed";
        string gateYellow = "GateYellow";

        if(prefabs == null)
            prefabs = new Dictionary<ETile, GameObject>();

        prefabs.Add(ETile.EMPTY, Resources.Load<GameObject>(basePath + empty));
        prefabs.Add(ETile.WALL, Resources.Load<GameObject>(basePath + wall));
        prefabs.Add(ETile.GOAL, Resources.Load<GameObject>(basePath + goal));
        prefabs.Add(ETile.BUTTON_BLUE, Resources.Load<GameObject>(basePath + buttonsRoot + buttonBlue));
        prefabs.Add(ETile.BUTTON_RED, Resources.Load<GameObject>(basePath + buttonsRoot + buttonRed));
        prefabs.Add(ETile.BUTTON_YELLOW, Resources.Load<GameObject>(basePath + buttonsRoot + buttonYellow));
        prefabs.Add(ETile.GATE_BLUE, Resources.Load<GameObject>(basePath + gatesRoot + gateBlue));
        prefabs.Add(ETile.GATE_RED, Resources.Load<GameObject>(basePath + gatesRoot + gateRed));
        prefabs.Add(ETile.GATE_YELLOW, Resources.Load<GameObject>(basePath + gatesRoot + gateYellow));
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
