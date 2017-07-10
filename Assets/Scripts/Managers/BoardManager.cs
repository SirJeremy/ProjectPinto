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
    private GameBoard board;
    private bool hasLevelSpawned = false;

    public GameBoard GameBoard { get { return board; } }

    private void Start() {
        if(spawnLevelAtStart) {
            BoardTileSet bts = levelData.GetLevel(worldIndex, levelIndex);
            if(bts != null)
                SpawnLevel(bts.playerStartingPosition, bts.tiles.To2DArray());
        }
    }
    public void SpawnLevel(IndexVector playerStartingLocation, ETile[,] tiles) {
        if(hasLevelSpawned) {
            Debug.LogWarning("Level is already spawned!");
            return;
        }
        if(tiles != null) {
            board = new GameBoard(tiles, playerStartingLocation);
            playerInstance = Player.SpawnPlayer(playerStartingLocation);
            hasLevelSpawned = true;
        }
    }
    public void DestroyLevel() {
        board.DestroyBoard();
        board = null;
        Destroy(playerInstance);
        playerInstance = null;
        hasLevelSpawned = false;
    }
}
