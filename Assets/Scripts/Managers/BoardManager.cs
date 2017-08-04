using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager> {
    #region Variables
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
    private LevelData currentLevelData; 
    private bool hasLevelSpawned = false;
    #endregion

    #region Properties
    public GameBoard GameBoard { get { return gameBoard; } }
    #endregion

    #region MonoBehaviours
    private void OnEnable() {
        EventManager.OnGoalReached += OnGoalReached;
    }
    private void OnDisable() {
        EventManager.OnGoalReached -= OnGoalReached;
    }
    private void Start() {
        if(spawnLevelAtStart) {
            currentLevelData = levelData.GetLevel(worldIndex, levelIndex);
            if(currentLevelData != null)
                SpawnLevel(currentLevelData);
        }
    }
    #endregion

    #region Methods
    public void SpawnLevel(LevelData levelData) {
        if(hasLevelSpawned) {
            Debug.LogWarning("Level is already spawned!");
            return;
        }
        if(levelData.tiles != null) {
            currentLevelData = levelData;
            gameBoard = new GameBoard(levelData.tiles, levelData.playerStartingPosition);
            playerInstance = Player.SpawnPlayer(levelData.playerStartingPosition);
            hasLevelSpawned = true;
            CameraFocus.Instance.FocusCamera(gameBoard.Width, gameBoard.Height, levelData.hasUIButtons);
            if(levelData.hasUIButtons)
                EventManager.AnnounceOnUIButtonControllerInitialize(levelData.uiButtonColors);
        }
    }
    private void DestroyLevel() {
        gameBoard.DestroyBoard();
        gameBoard = null;
        Destroy(playerInstance);
        playerInstance = null;
        hasLevelSpawned = false;
        EventManager.AnnounceOnUIButtonControllerInitialize(null);
    }

    private void OnGoalReached() {
        NotificationWindow.Instance.ShowNotification(currentLevelData.levelName + " Complete", new string[] { "Back to level select" }, 0, CallbackNotification, true);
    }
    private void CallbackNotification(int value) {
        DestroyLevel();
        EventManager.AnnounceOnGameExit();
    }
    #endregion
}
