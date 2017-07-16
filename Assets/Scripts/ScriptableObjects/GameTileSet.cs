using UnityEngine;

[CreateAssetMenu(fileName = "NewGameTileSet", menuName = "Custom/Game Tile Set")]
public class GameTileSet : ScriptableObject {
    [System.Serializable]
    private class GameTilePair {
        public ETile type = ETile.EMPTY;
        public GameObject prefab = null;
    }

    [SerializeField]
    private GameTilePair[] gameTiles = new GameTilePair[0];

    public GameObject GetPrefab(ETile type) {
        for(int i = 0; i < gameTiles.Length; i++) {
            if(gameTiles[i].type == type)
                return gameTiles[i].prefab;
        }
        Debug.LogError("No prefab for " + type + " was found");
        return null;
    }
}
