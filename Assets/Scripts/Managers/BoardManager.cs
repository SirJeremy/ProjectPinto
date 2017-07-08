using UnityEngine;

public class BoardManager : MonoSingleton<BoardManager> {
    [SerializeField]
    private GameObject player;
    private GameBoard board;
    public GameBoard GameBoard { get { return board; } }

    private void Start() {
        IndexVector playerStartingLocation = IndexVector.Zero;
        board = new GameBoard(new ETile[2,2] { {ETile.EMPTY, ETile.EMPTY }, {ETile.WALL, ETile.GOAL } }, playerStartingLocation);
        Player.SpawnPlayer(playerStartingLocation);
    }
}
