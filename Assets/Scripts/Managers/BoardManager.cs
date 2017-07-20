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
                SpawnLevel(bts);
        }
    }
    public void SpawnLevel(BoardTileSet bts) {
        if(hasLevelSpawned) {
            Debug.LogWarning("Level is already spawned!");
            return;
        }
        if(bts.tiles != null) {
            gameBoard = new GameBoard(bts.tiles, bts.playerStartingPosition);
            playerInstance = Player.SpawnPlayer(bts.playerStartingPosition);
            hasLevelSpawned = true;
            CameraFocus.Instance.FocusCamera(gameBoard.Width, gameBoard.Height, bts.hasUIButtons);
            if(bts.hasUIButtons)
                EventManager.AnnounceOnUIButtonControllerInitialize(bts.uiButtonColors.Length, bts.uiButtonColors);
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
