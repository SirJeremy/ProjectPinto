using UnityEngine;
public class GameBoard {
    #region Variables
    private BoardTile[,] board;
    private GameObject border;
    private int width = 0;
    private int height = 0;
    private IndexVector currentPlayerPosition = IndexVector.Zero;
    #endregion

    #region Properties
    public BoardTile[,] Board { get { return board; } }
    public int Width { get { return width; } }
    public int Height { get { return height; } }
    public IndexVector CurrentPlayerPosition { get { return currentPlayerPosition; } }
    #endregion

    #region Constructors
    // When [,] is initailised via { { }, { } }. It is done like this:
    // { { x:0y:0, x:0;y:1 },  { x:1y:0, x:1y:1 } }, or
    //  x:0y:1   x:1y:1
    //  x:0y:0   x:1y:0
    // in game, y is substituted for z
    public GameBoard(TileSetData tiles, IndexVector startingPosition) {
        //Create Board
        width = tiles.width;
        height = tiles.height;
        board = new BoardTile[width, height];
        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                board[i, j] = new BoardTile(tiles.rows[i].column[j].type, tiles.rows[i].column[j].color, new IndexVector(i, j));
            }
        }
        //Spawn Boarder
        border = GameTileSpawner.SpawnGameBoardBorder(width, height);
        //Set player starting position
        currentPlayerPosition = startingPosition;
        board[startingPosition.X, startingPosition.Y].IsOccupiedByPlayer = true;
    }
    #endregion

    #region Methods
    public void DestroyBoard() {
        for(int i = 0; i < width; i++) {
            for(int j = 0; i < height; j++) {
                board[i, j].DestroyTile();
            }
        }
        board = null;
        Object.Destroy(border);
        border = null;
    }
    public bool CanMoveInDirection(EDirection direction) {
        switch(direction) {
            case EDirection.UP:
                if(currentPlayerPosition.Y < height - 1) {
                    IndexVector destination = currentPlayerPosition + GetDirection(direction);
                    return board[destination.X, destination.Y].IsTraversable;
                }
                return false;
            case EDirection.DOWN:
                if(currentPlayerPosition.Y > 0) {
                    IndexVector destination = currentPlayerPosition + GetDirection(direction);
                    return board[destination.X, destination.Y].IsTraversable;
                }
                return false;
            case EDirection.LEFT:
                if(currentPlayerPosition.X > 0) {
                    IndexVector destination = currentPlayerPosition + GetDirection(direction);
                    return board[destination.X, destination.Y].IsTraversable;
                }
                return false;
            case EDirection.RIGHT:
                if(currentPlayerPosition.X < width - 1) {
                    IndexVector destination = currentPlayerPosition + GetDirection(direction);
                    return board[destination.X, destination.Y].IsTraversable;
                }
                return false;
            default:
                return false;
        }
        
    }
    public void StartMovePlayer(IndexVector destination) {
        board[destination.X, destination.Y].IsPlayerIsMovingIn = true;
    }
    public void StartMovePlayer(EDirection direction) {
        StartMovePlayer(currentPlayerPosition + GetDirection(direction));
    }
    public void FinishMovePlayer(IndexVector destination) {
        board[destination.X, destination.Y].IsPlayerIsMovingIn = false;
        board[destination.X, destination.Y].IsOccupiedByPlayer = true;
        board[currentPlayerPosition.X, currentPlayerPosition.Y].IsOccupiedByPlayer = false;
        currentPlayerPosition = destination;
    }
    public void FinishMovePlayer(EDirection direction) {
        FinishMovePlayer(currentPlayerPosition + GetDirection(direction));
    }
    private IndexVector GetDirection(EDirection direction) /* Need better name */ {
        switch(direction) {
            case EDirection.UP:
                return IndexVector.Up;
            case EDirection.DOWN:
                return IndexVector.Down;
            case EDirection.LEFT:
                return IndexVector.Left;
            case EDirection.RIGHT:
                return IndexVector.Right;
            default:
                return IndexVector.Up;
        }
    }
    #endregion
}
