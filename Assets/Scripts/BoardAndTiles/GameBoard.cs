﻿using UnityEngine;
public class GameBoard {
    #region Variables
    private BoardTile[,] board;
    private int boardWidth = 0;
    private int boardHeight = 0;
    private IndexVector currentPlayerPosition = IndexVector.Zero;
    #endregion

    #region Properties
    public BoardTile[,] Board { get { return board; } }
    #endregion

    #region Constructors
    // When [,] is initailised via { { }, { } }. It is done like this:
    // { { x:0y:0, x:0;y:1 },  { x:1y:0, x:1y:1 } }, or
    //  x:0y:1   x:1y:1
    //  x:0y:0   x:1y:0
    // in game, y is substituted for z
    public GameBoard(TileSetData tiles, IndexVector startingPosition) {
        //Create Board
        boardWidth = tiles.width;
        boardHeight = tiles.height;
        board = new BoardTile[boardWidth, boardHeight];
        for(int i = 0; i < boardWidth; i++) {
            for(int j = 0; j < boardHeight; j++) {
                board[i, j] = new BoardTile(tiles.rows[i].column[j].type, tiles.rows[i].column[j].color, new IndexVector(i, j));
            }
        }
        //Set player starting position
        currentPlayerPosition = startingPosition;
        board[startingPosition.X, startingPosition.Y].IsOccupiedByPlayer = true;
    }
    #endregion

    #region Methods
    public void DestroyBoard() {
        for(int i = 0; i < boardWidth; i++) {
            for(int j = 0; i < boardHeight; j++) {
                board[i, j].DestroyTile();
            }
        }
        board = null;
    }
    public bool CanMoveInDirection(EDirection direction) {
        switch(direction) {
            case EDirection.UP:
                if(currentPlayerPosition.Y < boardHeight - 1) {
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
                if(currentPlayerPosition.X < boardWidth - 1) {
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
