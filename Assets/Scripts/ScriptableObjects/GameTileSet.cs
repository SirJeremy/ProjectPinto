using UnityEngine;

[CreateAssetMenu(fileName = "NewGameTileSet", menuName = "Custom/Game Tile Set")]
public class GameTileSet : ScriptableObject {
    [System.Serializable]
    public class GameTilePair {
        public ETile type = ETile.EMPTY;
        public GameObject prefab = null;
    }

    [SerializeField]
    private GameTilePair[] gameTiles = new GameTilePair[0];

    public GameTilePair[] GameTilePairs { get { return gameTiles; } }

}
