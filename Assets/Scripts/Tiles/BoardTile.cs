using UnityEngine;

public class BoardTile {
    private bool isOccupiedByPlayer = false;
    private bool playerIsMovingIn = false;
    private bool isTraversable = true;
    private ETile tileType = ETile.EMPTY;
    private IndexVector location = IndexVector.Zero;
    private GameObject gameTile = null;
    
    public bool IsOccupiedByPlayer { get { return isOccupiedByPlayer; } set { isOccupiedByPlayer = value; } }
    public bool IsPlayerIsMovingIn { get { return playerIsMovingIn; } set { playerIsMovingIn = value; } }
    public bool IsTraversable { get { return isTraversable; } set { isTraversable = value; } }
    public IndexVector Location { get { return location; } }
    public ETile Type { get { return tileType; } }
    public GameObject GameTile { get { return gameTile; } }

    public BoardTile(ETile tileType, IndexVector location) {
        this.tileType = tileType;
        this.location = location;
        gameTile = GameTileSpawner.SpawnGameTile(tileType, location);
    }
    public void DestroyGameTile() {
        Object.Destroy(gameTile);
        gameTile = null; //is neccessary?
    }
}
