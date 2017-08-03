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
    [SerializeField]
    private GameObject levelSelect = null;

    private GameObject playerInstance;
    private GameBoard gameBoard;
    private bool hasLevelSpawned = false;

    public GameBoard GameBoard { get { return gameBoard; } }

    private void Start() {
        if(spawnLevelAtStart) {
            LevelData bts = levelData.GetLevel(worldIndex, levelIndex);
            if(bts != null)
                SpawnLevel(bts);
        }
    }
    public void SpawnLevel(LevelData ld) {
        if(hasLevelSpawned) {
            Debug.LogWarning("Level is already spawned!");
            return;
        }
        if(ld.tiles != null) {
            gameBoard = new GameBoard(ld.tiles, ld.playerStartingPosition);
            playerInstance = Player.SpawnPlayer(ld.playerStartingPosition);
            hasLevelSpawned = true;
            CameraFocus.Instance.FocusCamera(gameBoard.Width, gameBoard.Height, ld.hasUIButtons);
            if(ld.hasUIButtons)
                EventManager.AnnounceOnUIButtonControllerInitialize(ld.uiButtonColors.Length, ld.uiButtonColors);
        }
    }
    public void DestroyLevel() {
        gameBoard.DestroyBoard();
        gameBoard = null;
        Destroy(playerInstance);
        playerInstance = null;
        hasLevelSpawned = false;
    }
    public void TMPLevelSelect(int levelIndex) {
        levelSelect.SetActive(false);
        SpawnLevel(levelData.GetLevel(0, levelIndex));
    }
    public void TMPExitToLevelSelect() {
        DestroyLevel();
        levelSelect.SetActive(true);
    }
}
