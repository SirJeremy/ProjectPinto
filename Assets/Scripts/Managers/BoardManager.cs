using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager> {
    [SerializeField]
    private WorldSet levelData;
    [SerializeField]
    private bool spawnLevelAtStart = false;
    [SerializeField]
    private int worldIndex = 0;
    [SerializeField]
    private int levelIndex = 0;

    private GameObject playerInstance;
    private GameBoard gameBoard;
    private bool hasLevelSpawned = false;

    public GameBoard GameBoard { get { return gameBoard; } }

    private void Start() {
        if(spawnLevelAtStart) {
            BoardTileSet bts = levelData.GetLevel(worldIndex, levelIndex);
            if(bts != null)
                SpawnLevel(bts.playerStartingPosition, bts.tiles);
        }
    }
    public void SpawnLevel(IndexVector playerStartingLocation, TileSetData tiles) {
        if(hasLevelSpawned) {
            Debug.LogWarning("Level is already spawned!");
            return;
        }
        if(tiles != null) {
            gameBoard = new GameBoard(tiles, playerStartingLocation);
            playerInstance = Player.SpawnPlayer(playerStartingLocation);
            hasLevelSpawned = true;
        }
    }
    public void DestroyLevel() {
        gameBoard.DestroyBoard();
        gameBoard = null;
        Destroy(playerInstance);
        playerInstance = null;
        hasLevelSpawned = false;
    }
}
