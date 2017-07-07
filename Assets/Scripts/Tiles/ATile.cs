using UnityEngine;

public class Tile {
    private bool isOccupiedByPlayer = false;
    private bool playerIsMovingIn = false;
    protected bool isTraversable = true;
    private ETile tileType = ETile.EMPTY;
    
    public bool IsOccupiedByPlayer { get { return isOccupiedByPlayer; } set { isOccupiedByPlayer = value; } }
    public bool PlayerIsMovingIn { get { return playerIsMovingIn; } set { playerIsMovingIn = value; } }
    public ETile Type { get { return tileType; } set { tileType = value; } }

    public bool IsTraversable { get { return isTraversable; } }

    public Tile(ETile tileType = ETile.EMPTY) {
        this.tileType = tileType;
    }
}
