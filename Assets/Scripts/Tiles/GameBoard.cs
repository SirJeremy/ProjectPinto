public class GameBoard {
    private BoardTile[,] board;
    private int boardWidth = 0;
    private int boardHeight = 0;
    private IndexVector currentPlayerPosition = IndexVector.Zero;

    public BoardTile[,] Board { get { return board; } }

    public GameBoard(ETile[,] tiles, IndexVector startingPosition) {
        //tiles >>> board
        boardWidth = tiles.GetLength(0);
        boardHeight = tiles.GetLength(1);
        board = new BoardTile[boardWidth, boardHeight];
        for(int i = 0; i < boardWidth; i++) {
            for(int j = 0; i < boardHeight; j++) {
                board[i, j] = new BoardTile(tiles[i, j], new IndexVector(i, j));
            }
        }
        board[startingPosition.X, startingPosition.Y].IsOccupiedByPlayer = true;
    }
    public bool CanMoveInDirection(EDirection direction) {
        switch(direction) {
            case EDirection.UP:
                if(currentPlayerPosition.Y < boardHeight) {
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
                if(currentPlayerPosition.Y < boardWidth) {
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
}
