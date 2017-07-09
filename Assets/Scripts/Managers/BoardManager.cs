using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager> {
    private GameObject playerInstance;
    private GameBoard board;

    public GameBoard GameBoard { get { return board; } }

    private void Start() {
        SpawnLevel(IndexVector.Zero, new ETile[2, 2] { { ETile.EMPTY, ETile.EMPTY }, { ETile.WALL, ETile.GOAL } });
    }
    public void SpawnLevel(IndexVector playerStartingLocation, ETile[,] tiles) {
        board = new GameBoard(tiles, playerStartingLocation);
        playerInstance = Player.SpawnPlayer(playerStartingLocation);
    }
    public void DestroyLevel() {
        board.DestroyBoard();
        board = null;
        Destroy(playerInstance);
        playerInstance = null;
    }
}
